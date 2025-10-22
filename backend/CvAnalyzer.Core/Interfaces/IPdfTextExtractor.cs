namespace CvAnalyzer.Core.Interfaces;

public interface IPdfTextExtractor
{
    Task<string> ExtractTextFromPdfAsync(Stream pdfStream);
}