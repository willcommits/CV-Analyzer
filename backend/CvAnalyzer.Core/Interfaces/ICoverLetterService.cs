namespace CvAnalyzer.Core.Interfaces;

public interface ICoverLetterService
{
    Task<string> GenerateCoverLetterAsync(string cvText, string jobDescription);
}