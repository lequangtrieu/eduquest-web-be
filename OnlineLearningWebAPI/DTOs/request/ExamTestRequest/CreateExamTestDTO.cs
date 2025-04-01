using Microsoft.AspNetCore.Mvc;
using OnlineLearningWebAPI.DTOs.QuizRequest;
using System.Text.Json.Serialization;

namespace OnlineLearningWebAPI.DTOs.ExamTestRequest
{
    public class CreateExamTestDTO
    {
        public int MoocId { get; set; }
        public string? TestName { get; set; }
        public int Duration { get; set; }
        public IFormFile? Video {  get; set; }
        public string CreatedBy { get; set; }
    }
    public class CreateExamTestWithVideoDTO
    {
        public string? TestName { get; set; }
        public IFormFile? Video { get; set; }
        public ICollection<CreateFullQuizDTO>? Quizzes { get; set; } = new List<CreateFullQuizDTO>();
    }
}
