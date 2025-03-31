using OnlineLearningWebAPI.DTOs.Response.QuizAnswerResponse;

namespace OnlineLearningWebAPI.DTOs.Response.QuizResponse
{
    public class QuizDTO
    {
        public int QuizId { get; set; }
        public int ExamTestId { get; set; }
        public string? QuizName { get; set; }
        public string? QuizQuestion { get; set; }
        public int QuizTypeId { get; set; }
        public int MaxScore { get; set; }
        public IEnumerable<QuizAnswerDTO>? Answers { get; set; }
    }
}
