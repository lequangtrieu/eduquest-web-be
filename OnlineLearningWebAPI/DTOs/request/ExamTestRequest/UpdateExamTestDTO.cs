using OnlineLearningWebAPI.DTOs.QuizRequest;

namespace OnlineLearningWebAPI.DTOs.ExamTestRequest
{
    public class UpdateExamTestDTO
    {
        public string? TestName { get; set; }
        public int? Duration { get; set; }
    }

    public class UpdateExamTestWithVideoDTO
    {
        public int? ExamTestId { get; set; }
        public string? TestName { get; set; }
        public IFormFile? Video { get; set; }
        public ICollection<UpdateFullQuizDTO>? Quizzes { get; set; } = new List<UpdateFullQuizDTO>();
    }
}
