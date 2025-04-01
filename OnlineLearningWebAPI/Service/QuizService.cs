using OnlineLearningWebAPI.DTOs.QuizRequest;
using OnlineLearningWebAPI.DTOs.Response.QuizResponse;
using OnlineLearningWebAPI.DTOs.Response.QuizAnswerResponse;
using OnlineLearningWebAPI.Models;
using OnlineLearningWebAPI.Repository.IRepository;
using OnlineLearningWebAPI.Service.IService;

namespace OnlineLearningWebAPI.Service
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizAnswerRepository _quizAnswerRepository;

        public QuizService(IQuizRepository quizRepository,
            IQuizAnswerRepository quizAnswerRepository)
        {
            _quizRepository = quizRepository;
            _quizAnswerRepository = quizAnswerRepository;
        }

        public async Task<IEnumerable<QuizDTO>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync();
            return quizzes.Select(q => new QuizDTO
            {
                QuizId = q.QuizId,
                ExamTestId = q.ExamTestId,
                QuizName = q.QuizName,
                QuizQuestion = q.QuizQuestion,
                QuizTypeId = q.QuizTypeId,
                MaxScore = q.MaxScore
            });
        }

        public async Task<QuizDTO?> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null) return null;
            
            return new QuizDTO
            {
                QuizId = quiz.QuizId,
                ExamTestId = quiz.ExamTestId,
                QuizName = quiz.QuizName,
                QuizQuestion = quiz.QuizQuestion,
                QuizTypeId = quiz.QuizTypeId,
                MaxScore = quiz.MaxScore
            };
        }

        public async Task<bool> CreateQuizAsync(CreateQuizDTO createQuizDTO)
        {
            var quiz = new Quiz
            {
                ExamTestId = createQuizDTO.ExamTestId,
                QuizName = createQuizDTO.QuizName,
                QuizQuestion = createQuizDTO.QuizQuestion,
                QuizTypeId = createQuizDTO.QuizTypeId,
                MaxScore = createQuizDTO.MaxScore
            };

            await _quizRepository.AddAsync(quiz);
            await _quizRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateQuizAsync(int id, UpdateQuizDTO updateQuizDTO)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null) return false;

            quiz.QuizName = updateQuizDTO.QuizName ?? quiz.QuizName;
            quiz.QuizQuestion = updateQuizDTO.QuizQuestion ?? quiz.QuizQuestion;
            quiz.MaxScore = updateQuizDTO.MaxScore ?? quiz.MaxScore;

            _quizRepository.Update(quiz);
            await _quizRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteQuizAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null) return false;

            var quizAnswers = await _quizAnswerRepository.GetAnswersByQuizIdAsync(quiz.QuizId);
            if (quizAnswers.Any())
            {
                foreach (var answer in quizAnswers)
                {
                    _quizAnswerRepository.Delete(answer);
                }
                await _quizAnswerRepository.SaveChangesAsync();
            }
            _quizRepository.Delete(quiz);
            await _quizRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<QuizDTO>> GetQuizzesByExamTestIdAsync(int examTestId)
        {
            var quizzes = await _quizRepository.GetByExamTestIdAsync(examTestId);

            var quizIds = quizzes.Select(q => q.QuizId).ToList();
            var allAnswers = new List<QuizAnswer>();
            foreach (var item in quizIds)
            {
                var answer = await _quizAnswerRepository.GetAnswersByQuizIdAsync(item);
                allAnswers.AddRange(answer);
            }

            var quizList = quizzes.Select(q => new QuizDTO
            {
                QuizId = q.QuizId,
                ExamTestId = q.ExamTestId,
                QuizName = q.QuizName,
                QuizQuestion = q.QuizQuestion,
                QuizTypeId = q.QuizTypeId,
                MaxScore = q.MaxScore,
                Answers = allAnswers
                    .Where(a => a.QuizId == q.QuizId)
                    .Select(i => new QuizAnswerDTO
                    {
                        QuizId = i.QuizId,
                        Answer = i.Answer,
                        IsCorrect = i.IsCorrect,
                        QuizAnswerId = i.QuizAnswerId
                    })
            });

            return quizList;
        }

        public async Task<int> CreateQuizGetIdAsync(CreateQuizDTO createQuizDTO)
        {
            var quiz = new Quiz
            {
                ExamTestId = createQuizDTO.ExamTestId,
                QuizName = createQuizDTO.QuizName,
                QuizQuestion = createQuizDTO.QuizQuestion,
                QuizTypeId = createQuizDTO.QuizTypeId,
                MaxScore = createQuizDTO.MaxScore
            };

            await _quizRepository.AddAsync(quiz);
            await _quizRepository.SaveChangesAsync();
            return quiz.QuizId;
        }
    }
}