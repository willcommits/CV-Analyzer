using CvAnalyzer.Core.Interfaces;
using OpenAI;
using OpenAI.Chat;

namespace CvAnalyzer.Infrastructure.Services;

public class OpenAICoverLetterService : ICoverLetterService
{
    private readonly OpenAIClient _openAIClient;

    public OpenAICoverLetterService(OpenAIClient openAIClient)
    {
        _openAIClient = openAIClient;
    }

    public async Task<string> GenerateCoverLetterAsync(string cvText, string jobDescription)
    {
        try
        {
            var prompt = CreateCoverLetterPrompt(cvText, jobDescription);
            
            var chatClient = _openAIClient.GetChatClient("gpt-3.5-turbo");
            
            var response = await chatClient.CompleteChatAsync(
                [
                    ChatMessage.CreateSystemMessage("You are a professional career advisor and expert cover letter writer. Create compelling, personalized cover letters that highlight the most relevant qualifications for specific job opportunities."),
                    ChatMessage.CreateUserMessage(prompt)
                ]
            );

            return response.Value.Content[0].Text.Trim();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to generate cover letter: {ex.Message}", ex);
        }
    }

    private static string CreateCoverLetterPrompt(string cvText, string jobDescription)
    {
        return $@"Based on the following CV and job description, write a professional cover letter that is 2-3 paragraphs long. The cover letter should:

1. Highlight the most relevant experience and skills from the CV that match the job requirements
2. Demonstrate enthusiasm for the specific role and company
3. Be professional, engaging, and personalized
4. Avoid generic statements and focus on specific achievements and qualifications
5. Show clear understanding of how the candidate's background aligns with the job requirements

CV:
{cvText}

Job Description:
{jobDescription}

Please write a compelling cover letter that would make this candidate stand out for this specific position:";
    }
}