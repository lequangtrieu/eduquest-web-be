using OnlineLearningWebAPI.DTOs.ExamTestRequest;
using OnlineLearningWebAPI.DTOs.Response.ExamTestResponse;
using OnlineLearningWebAPI.Models;
using OnlineLearningWebAPI.Repository.IRepository;
using OnlineLearningWebAPI.Service.IService;

namespace OnlineLearningWebAPI.Service
{
    public class ExamTestService : IExamTestService
    {
        private readonly IExamTestRepository _examTestRepository;
        private readonly IQuizService _quizService;
        private readonly IFirebaseService _firebaseService;

        public ExamTestService(IExamTestRepository examTestRepository,
            IQuizService quizService,
            IFirebaseService firebaseService)
        {
            _examTestRepository = examTestRepository;
            _quizService = quizService;
            _firebaseService = firebaseService;
        }

        public async Task<IEnumerable<ExamTestDTO>> GetAllExamTestsAsync()
        {
            var examTests = await _examTestRepository.GetAllAsync();
            return examTests.Select(e => new ExamTestDTO
            {
                ExamTestId = e.ExamTestId,
                MoocId = e.MoocId,
                TestName = e.TestName,
                Duration = e.Duration,
                CreatedBy = e.CreatedBy
            });
        }

        public async Task<ExamTestDTO?> GetExamTestByIdAsync(int id)
        {
            var examTest = await _examTestRepository.GetByIdAsync(id);
            if (examTest == null) return null;

            return new ExamTestDTO
            {
                ExamTestId = examTest.ExamTestId,
                MoocId = examTest.MoocId,
                TestName = examTest.TestName,
                Duration = examTest.Duration,
                CreatedBy = examTest.CreatedBy
            };
        }

        public async Task<bool> CreateExamTestAsync(CreateExamTestDTO createExamTestDTO)
        {
            var examTest = new ExamTest
            {
                MoocId = createExamTestDTO.MoocId,
                TestName = createExamTestDTO.TestName,
                Duration = createExamTestDTO.Duration,
                CreatedBy = createExamTestDTO.CreatedBy
            };

            if (createExamTestDTO.Video != null)
            {
                string videoUrl = await _firebaseService.UploadVideo(createExamTestDTO.Video);
                examTest.VideoUrl = videoUrl;
            }

            await _examTestRepository.AddAsync(examTest);
            await _examTestRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateExamTestAsync(int id, UpdateExamTestDTO updateExamTestDTO)
        {
            var examTest = await _examTestRepository.GetByIdAsync(id);
            if (examTest == null) return false;

            examTest.TestName = updateExamTestDTO.TestName ?? examTest.TestName;
            examTest.Duration = updateExamTestDTO.Duration ?? examTest.Duration;

            _examTestRepository.Update(examTest);
            await _examTestRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExamTestAsync(int id)
        {
            var examTest = await _examTestRepository.GetByIdAsync(id);
            if (examTest == null) return false;

            var quizzes = await _quizService.GetQuizzesByExamTestIdAsync(examTest.ExamTestId);
            if(quizzes.Any())
            {
                foreach (var quizz in quizzes)
                {
                    await _quizService.DeleteQuizAsync(quizz.QuizId);
                }
            }
            _examTestRepository.Delete(examTest);
            await _examTestRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ExamTestDTO>> GetExamTestsByMoocIdAsync(int moocId)
        {
            var examTests = await _examTestRepository.GetByMoocIdAsync(moocId);
            return examTests.Select(e => new ExamTestDTO
            {
                ExamTestId = e.ExamTestId,
                MoocId = e.MoocId,
                TestName = e.TestName,
                Duration = e.Duration,
                CreatedBy = e.CreatedBy,
                VideoURL = e.VideoUrl,
                IsQuiz = e.VideoUrl == null
            });
        }

        public async Task<int> CreateExamTestGetIdAsync(CreateExamTestDTO createExamTestDTO)
        {
            var examTest = new ExamTest
            {
                MoocId = createExamTestDTO.MoocId,
                TestName = createExamTestDTO.TestName,
                Duration = createExamTestDTO.Duration,
                CreatedBy = createExamTestDTO.CreatedBy
            };

            if (createExamTestDTO.Video != null)
            {
                string videoUrl = await _firebaseService.UploadVideo(createExamTestDTO.Video);
                examTest.VideoUrl = videoUrl;
            }

            await _examTestRepository.AddAsync(examTest);
            await _examTestRepository.SaveChangesAsync();
            return examTest.ExamTestId;
        }
    }
}
