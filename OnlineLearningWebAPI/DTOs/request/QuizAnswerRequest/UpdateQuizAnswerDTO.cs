namespace OnlineLearningWebAPI.DTOs.QuizAnswerRequest
{
    public class UpdateQuizAnswerDTO
    {
        public string? Answer { get; set; }
        public bool? IsCorrect { get; set; }
    }

    public class UpdateItemQuizAnswerDTO
    {
        public int? QuizAnswerId { get; set; }
        public string? Answer { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
