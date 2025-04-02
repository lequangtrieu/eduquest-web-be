using OnlineLearningWebAPI.DTOs.QuizAnswerRequest;

namespace OnlineLearningWebAPI.DTOs.QuizRequest
{
    public class UpdateQuizDTO
    {
        public string? QuizName { get; set; }
        public string? QuizQuestion { get; set; }
        public int? MaxScore { get; set; }
    }

    public class UpdateFullQuizDTO
    {
        public int? QuizId { get; set; }
        public string? QuizName { get; set; }
        public string? QuizQuestion { get; set; }
        public ICollection<UpdateItemQuizAnswerDTO> Answers { get; set; } = new List<UpdateItemQuizAnswerDTO>();
    }
}
