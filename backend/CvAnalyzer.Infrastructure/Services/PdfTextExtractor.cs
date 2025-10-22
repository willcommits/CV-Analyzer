using CvAnalyzer.Core.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace CvAnalyzer.Infrastructure.Services;

public class PdfTextExtractor : IPdfTextExtractor
{
    public async Task<string> ExtractTextFromPdfAsync(Stream pdfStream)
    {
        try
        {
            await Task.Yield(); // Make method async-friendly
            
            using var pdfDocument = new PdfDocument(new PdfReader(pdfStream));
            var text = new System.Text.StringBuilder();
            
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                var page = pdfDocument.GetPage(i);
                var strategy = new SimpleTextExtractionStrategy();
                var pageText = iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(page, strategy);
                text.AppendLine(pageText);
            }
            
            return text.ToString().Trim();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to extract text from PDF: {ex.Message}", ex);
        }
    }
}