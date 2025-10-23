using CvAnalyzer.Core.Interfaces;
using CvAnalyzer.Core.Models;
using CvAnalyzer.Infrastructure.Services;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Configure port for Render deployment
var port = Environment.GetEnvironmentVariable("PORT") ?? "5161";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173", 
                "http://localhost:5174", 
                "http://localhost:3000",
                "https://*.onrender.com") // React dev servers and production
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Configure OpenAI client
var openAiApiKey = builder.Configuration["OpenAI:ApiKey"] 
    ?? Environment.GetEnvironmentVariable("OpenAI__ApiKey")
    ?? throw new InvalidOperationException("OpenAI API key not configured. Please set OpenAI__ApiKey environment variable or OpenAI:ApiKey in appsettings.json");
builder.Services.AddSingleton<OpenAIClient>(new OpenAIClient(openAiApiKey));

// Register services
builder.Services.AddScoped<IPdfTextExtractor, PdfTextExtractor>();
builder.Services.AddScoped<ICoverLetterService, OpenAICoverLetterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Only use HTTPS redirection in development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseCors();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
    .WithName("HealthCheck")
    .WithOpenApi();

// CV Analysis endpoint
app.MapPost("/api/analyze-cv", async (
    HttpRequest request,
    IPdfTextExtractor pdfExtractor,
    ICoverLetterService coverLetterService) =>
{
    try
    {
        if (!request.HasFormContentType)
        {
            return Results.BadRequest("Request must be multipart/form-data");
        }

        var form = await request.ReadFormAsync();
        
        // Extract job description
        var jobDescription = form["jobDescription"].ToString();
        if (string.IsNullOrWhiteSpace(jobDescription))
        {
            return Results.BadRequest("Job description is required");
        }

        // Extract CV file
        var pdfFile = form.Files["cvFile"];
        if (pdfFile == null || pdfFile.Length == 0)
        {
            return Results.BadRequest("CV PDF file is required");
        }

        // Validate file type
        if (pdfFile.ContentType != "application/pdf")
        {
            return Results.BadRequest("Only PDF files are allowed");
        }

        // Validate file size (5MB max)
        if (pdfFile.Length > 5 * 1024 * 1024)
        {
            return Results.BadRequest("File size cannot exceed 5MB");
        }

        // Extract text from PDF
        string cvText;
        using (var stream = pdfFile.OpenReadStream())
        {
            cvText = await pdfExtractor.ExtractTextFromPdfAsync(stream);
        }

        if (string.IsNullOrWhiteSpace(cvText))
        {
            return Results.BadRequest("Could not extract text from the PDF file");
        }

        // Generate cover letter
        var coverLetter = await coverLetterService.GenerateCoverLetterAsync(cvText, jobDescription);

        var response = new CvAnalysisResponse
        {
            CoverLetter = coverLetter,
            Success = true
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        var errorResponse = new CvAnalysisResponse
        {
            Success = false,
            ErrorMessage = $"An error occurred: {ex.Message}"
        };
        
        return Results.Json(errorResponse, statusCode: 500);
    }
})
.WithName("AnalyzeCV")
.WithOpenApi()
.DisableAntiforgery(); // Required for file uploads

app.Run();