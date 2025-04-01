using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineLearningWebAPI.DTOs.ExamTestRequest;
using OnlineLearningWebAPI.DTOs.QuizAnswerRequest;
using OnlineLearningWebAPI.DTOs.QuizRequest;
using OnlineLearningWebAPI.DTOs.request.CourseRequest;
using OnlineLearningWebAPI.Models;
using OnlineLearningWebAPI.Service;
using OnlineLearningWebAPI.Service.IService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace OnlineLearningWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IExamTestService _examTestService;
        private readonly IQuizService _quizService;
        private readonly IQuizAnswerService _quizAnswerService;
        private readonly IMoocService _moocService;
        private readonly ITeacherService _teacherService;

        public CourseController(ICourseService courseService,
            IExamTestService examTestService,
            IQuizService quizService,
            IQuizAnswerService quizAnswerService,
            IMoocService moocService,
            ITeacherService teacherService)
        {
            _courseService = courseService;
            _examTestService = examTestService;
            _quizService = quizService;
            _quizAnswerService = quizAnswerService;
            _moocService = moocService;
            _teacherService = teacherService;
        }

        [HttpGet("/api/Courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound(new { message = "Course not found" });

            return Ok(course);
        }

        [HttpGet("full/{id}")]
        public async Task<IActionResult> GetFullCourseById(int id)
        {
            var course = await _courseService.GetFullCourseByIdAsync(id);
            
            if (course == null) return NotFound(new { message = "Course not found" });

            return Ok(course);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCourse([FromForm] CreateCourseDTO createCourseDTO)
        {
            if (createCourseDTO.Image == null || createCourseDTO.Image.Length == 0)
            {
                return BadRequest("Image required.");
            }

            bool isValidFile = Helper.ValidationHelper.IsValidImage(createCourseDTO.Image);
            if (!isValidFile)
                return BadRequest("Invalid image file. File format allowed: .jpg, .jpeg, .png");
            var result = await _courseService.CreateCourseAsync(createCourseDTO);
            if (!result) return BadRequest(new { message = "Failed to create course" });

            return Ok(new { message = "Course created successfully" });
        }

        [HttpPost("full")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateFullCourse([FromForm] CreateFullCourseDTO createCourseDTO)
        {

            if (createCourseDTO.Image == null || createCourseDTO.Image.Length == 0)
            {
                return BadRequest("Image required.");
            }

            bool isValidFile = Helper.ValidationHelper.IsValidImage(createCourseDTO.Image);
            if (!isValidFile)
                return BadRequest("Invalid image file. File format allowed: .jpg, .jpeg, .png");
            try
            {
                var moocId = await _courseService.CreateCourseAndMoocAsync(
                    new CreateCourseDTO
                    {
                        CategoryId = createCourseDTO.CategoryId,
                        CourseTitle = createCourseDTO.CourseTitle,
                        Description = createCourseDTO.Description,
                        Image = createCourseDTO.Image,
                        Price = createCourseDTO.Price,
                        TeacherId = createCourseDTO.TeacherId
                    });

                var formData = Request.Form;
                int examIndex = 0;
                while (formData.ContainsKey($"ExamTests[{examIndex}].TestName"))
                {
                    var exam = new CreateExamTestDTO
                    {
                        MoocId = moocId,
                        TestName = formData[$"ExamTests[{examIndex}].TestName"],
                        Duration = 0,
                        CreatedBy = createCourseDTO.TeacherId
                    };
                    Console.WriteLine(formData.Files.Count);
                    Console.WriteLine(formData.ContainsKey($"ExamTests[{examIndex}].Video"));
                    if (formData.Files.Count > 0 && formData.Files.Any(f => f.Name == $"ExamTests[{examIndex}].Video"))
                    {

                        if (!Helper.ValidationHelper.IsValidVideo(formData.Files.FirstOrDefault(f => f.Name == $"ExamTests[{examIndex}].Video")))
                        {
                            return BadRequest("Invalid video file. File format allowed: .mp4");
                        }
                        exam.Video = formData.Files.FirstOrDefault(f => f.Name == $"ExamTests[{examIndex}].Video");
                    }
                    var examId = await _examTestService.CreateExamTestGetIdAsync(exam);

                    int quizIndex = 0;
                    while (formData.ContainsKey($"ExamTests[{examIndex}].Quizzes[{quizIndex}].QuizQuestion"))
                    {
                        var quiz = new CreateQuizDTO
                        {
                            ExamTestId = examId,
                            QuizName = "Q" + (quizIndex + 1),
                            QuizQuestion = formData[$"ExamTests[{examIndex}].Quizzes[{quizIndex}].QuizQuestion"]
                        };
                        var quizId = await _quizService.CreateQuizGetIdAsync(quiz);

                        int ansIndex = 0;
                        while (formData.ContainsKey($"ExamTests[{examIndex}].Quizzes[{quizIndex}].Answers[{ansIndex}].Answer"))
                        {
                            var answer = new CreateQuizAnswerDTO
                            {
                                QuizId = quizId,
                                Answer = formData[$"ExamTests[{examIndex}].Quizzes[{quizIndex}].Answers[{ansIndex}].Answer"],
                                IsCorrect = bool.Parse(formData[$"ExamTests[{examIndex}].Quizzes[{quizIndex}].Answers[{ansIndex}].IsCorrect"])
                            };
                            await _quizAnswerService.CreateAnswerAsync(answer);
                            ansIndex++;
                        }
                        quizIndex++;
                    }
                    examIndex++;
                }

                //foreach (var iExam in createCourseDTO.ExamTests)
                //{
                //    var exam = new CreateExamTestDTO
                //    {
                //        MoocId = moocId,
                //        CreatedBy = createCourseDTO.TeacherId,
                //        Duration = 0,
                //        TestName = iExam.TestName
                //    };
                //    var examId = await _examTestService.CreateExamTestGetIdAsync(exam);
                //    Console.WriteLine("Exam ID:" + examId);
                //    if (iExam.Quizzes.Count > 0)
                //    {
                //        foreach (var iQuiz in iExam.Quizzes)
                //        {
                //            var quiz = new CreateQuizDTO
                //            {
                //                ExamTestId = examId,
                //                QuizName = iQuiz.QuizName,
                //                QuizQuestion = iQuiz.QuizQuestion
                //            };
                //            var quizId = await _quizService.CreateQuizGetIdAsync(quiz);
                //            Console.WriteLine("Quiz ID:" + quizId);
                //            foreach (var iAns in iQuiz.Answers)
                //            {
                //                var ans = new CreateQuizAnswerDTO
                //                {
                //                    QuizId = quizId,
                //                    Answer = iAns.Answer,
                //                    IsCorrect = iAns.IsCorrect
                //                };
                //                await _quizAnswerService.CreateAnswerAsync(ans);
                //            }
                //        }
                //    }

                //}
            } catch(Exception ex)
            {
                return BadRequest("Failed to create course: " + ex.Message);
            }

            return Ok(new { message = "Course created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDTO updateCourseDTO)
        {
            var result = await _courseService.UpdateCourseAsync(id, updateCourseDTO);
            if (!result) return NotFound(new { message = "Course not found or update failed" });

            return Ok(new { message = "Course updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result) return NotFound(new { message = "Course not found or delete failed" });

            return Ok(new { message = "Course deleted successfully" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCourses([FromQuery] string keyword)
        {
            var courses = await _courseService.SearchCoursesAsync(keyword);
            if (courses == null || !courses.Any())
                return NotFound(new { message = "No courses found" });

            return Ok(courses);
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveCourse(int id)
        {
            var result = await _courseService.ApproveCourseAsync(id);
            if (!result) return BadRequest(new { message = "Course not found or not pending" });

            return Ok(new { message = "Course approved successfully" });
        }

        [HttpPut("unapprove/{id}")]
        public async Task<IActionResult> UnApproveCourse(int id)
        {
            var result = await _courseService.UnApproveCourseAsync(id);
            if (!result) return BadRequest(new { message = "Course not found or not pending" });

            return Ok(new { message = "Course approved successfully" });
        }
    }
}
