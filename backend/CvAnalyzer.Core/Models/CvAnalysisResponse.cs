namespace CvAnalyzer.Core.Models;

public class CvAnalysisResponse
{
    public string CoverLetter { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}