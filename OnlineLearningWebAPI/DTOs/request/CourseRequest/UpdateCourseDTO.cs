using Microsoft.AspNetCore.Mvc;
using OnlineLearningWebAPI.DTOs.ExamTestRequest;
using OnlineLearningWebAPI.Enum;

namespace OnlineLearningWebAPI.DTOs.request.CourseRequest
{
    public class UpdateCourseDTO
    {
        public string? CourseTitle { get; set; }
        public string? Description { get; set; }
        public string? TeacherId { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int? Price { get; set; }
        public IFormFile? Image { get; set; }
        public CourseStatus Status { get; set; }
    }

    public class UpdateFullCourseDTO
    {
        public string CourseTitle { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        [FromForm]
        public IFormFile? Image { get; set; }
        public ICollection<UpdateExamTestWithVideoDTO> ExamTests { get; set; } = new List<UpdateExamTestWithVideoDTO>();
    }
}
