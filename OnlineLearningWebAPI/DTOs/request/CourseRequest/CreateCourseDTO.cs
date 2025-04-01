using Microsoft.AspNetCore.Mvc;
using OnlineLearningWebAPI.DTOs.ExamTestRequest;
using System.Text.Json.Serialization;

namespace OnlineLearningWebAPI.DTOs.request.CourseRequest
{
    public class CreateCourseDTO
    {
        public string CourseTitle { get; set; } = null!;
        public string? Description { get; set; }
        public string TeacherId { get; set; } = null!;
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public IFormFile Image { get; set; }
    }

    public class CreateFullCourseDTO
    {
        public string CourseTitle { get; set; } = null!;
        public string? Description { get; set; }
        public string TeacherId { get; set; } = null!;
        public int CategoryId { get; set; }
        public int Price { get; set; }
        [FromForm]
        public IFormFile Image { get; set; }
        public ICollection<CreateExamTestWithVideoDTO> ExamTests { get; set; } = new List<CreateExamTestWithVideoDTO>();
    }
}
