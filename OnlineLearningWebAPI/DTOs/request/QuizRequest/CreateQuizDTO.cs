using OnlineLearningWebAPI.DTOs.QuizAnswerRequest;

namespace OnlineLearningWebAPI.DTOs.QuizRequest
{
    public class CreateQuizDTO
    {
        public int ExamTestId { get; set; }
        public string? QuizName { get; set; }
        public string? QuizQuestion { get; set; }
        public int QuizTypeId { get; set; } = 1;
        public int MaxScore { get; set; } = 10;
    }

    public class CreateFullQuizDTO
    {
        public string? QuizName { get; set; }
        public string? QuizQuestion { get; set; }
        public int? QuizTypeId { get; set; } = 1;
        public int? MaxScore { get; set; } = 10;
        public ICollection<CreateItemQuizAnswerDTO> Answers { get; set; } = new List<CreateItemQuizAnswerDTO>();
    }
}
