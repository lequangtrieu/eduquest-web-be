﻿using Microsoft.EntityFrameworkCore;
using OnlineLearningWebAPI.DTOs.request.CourseRequest;
using OnlineLearningWebAPI.DTOs.Response.CourseResponse;
using OnlineLearningWebAPI.Models;
using OnlineLearningWebAPI.Repository.IRepository;
using OnlineLearningWebAPI.Service.IService;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearningWebAPI.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseTagService _courseTagService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly IImageService _imageService;

        public CourseService(ICourseRepository courseRepository, 
            ICourseTagService courseTagService, 
            ICourseCategoryService courseCategoryService, 
            IImageService imageService)
        {
            _courseRepository = courseRepository;
            _courseTagService = courseTagService;
            _courseCategoryService = courseCategoryService;
            _imageService = imageService;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var categories = await _courseCategoryService.GetAllCategoriesAsync();
            return courses.Select(c => new CourseDTO
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                Description = c.Description,
                TeacherId = c.TeacherId,
                CreateDate = c.CreateDate,
                CategoryName = categories.First(cate => cate.CategoryId == c.CategoryId).Name,
                Price = c.Price,
                ImageURL = c.ImageURL,
                Status = c.Status,
            });
        }

        public async Task<CourseDTO?> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            var courseTags = await _courseTagService.GetTagsByCourseIdAsync(id);
            var categories = await _courseCategoryService.GetAllCategoriesAsync();

            if (course == null) return null;
            return new CourseDTO
            {
                CourseId = course.CourseId,
                CourseTitle = course.CourseTitle,
                Description = course.Description,
                TeacherId = course.TeacherId,
                CreateDate = course.CreateDate,
                Price = course.Price,
                CategoryName = categories.First(cate => cate.CategoryId == course.CategoryId).Name,
                ImageURL = course.ImageURL,
                Status = course.Status,
                CourseTags = courseTags,
            };
        }

        public async Task<bool> CreateCourseAsync(CreateCourseDTO createCourseDTO)
        {
            string imageUrl = await _imageService.UploadImage(createCourseDTO.Image);
            var course = new Course
            {
                CourseTitle = createCourseDTO.CourseTitle,
                Description = createCourseDTO.Description,
                TeacherId = createCourseDTO.TeacherId,
                CategoryId = createCourseDTO.CategoryId,
                Price = createCourseDTO.Price,
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                ImageURL = imageUrl
            };

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourseAsync(int id, UpdateCourseDTO updateCourseDTO)
        {
            string? imageUrl = null;
            if (updateCourseDTO.Image != null)
            {
                imageUrl = await _imageService.UploadImage(updateCourseDTO.Image);
            }

            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return false;

            course.CourseTitle = updateCourseDTO.CourseTitle ?? course.CourseTitle;
            course.Description = updateCourseDTO.Description ?? course.Description;
            course.CategoryId = updateCourseDTO.CategoryId ?? course.CategoryId;
            course.ImageURL = imageUrl ?? course.ImageURL;
            course.Price = updateCourseDTO?.Price ?? course.Price;
            course.TeacherId = updateCourseDTO?.TeacherId ?? course.TeacherId;
            course.Status = updateCourseDTO?.Status ?? course.Status;

            _courseRepository.Update(course);
            await _courseRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return false;

            _courseRepository.Delete(course);
            await _courseRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesByTeacherIdAsync(string teacherId)
        {
            var courses = await _courseRepository.GetCoursesByTeacherIdAsync(teacherId);
            var categories = await _courseCategoryService.GetAllCategoriesAsync();
            return courses.Select(course => new CourseDTO
            {
                CourseId = course.CourseId,
                CourseTitle = course.CourseTitle,
                Description = course.Description,
                TeacherId = course.TeacherId,
                CreateDate = course.CreateDate,
                CategoryName = categories.First(cate => cate.CategoryId == course.CategoryId).Name,
                Price = course.Price,
                ImageURL = course.ImageURL,
                Status = course.Status,
            });
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesByCategoryIdAsync(int categoryId)
        {
            var courses = await _courseRepository.GetCoursesByCategoryIdAsync(categoryId);
            var categories = await _courseCategoryService.GetAllCategoriesAsync();
            return courses.Select(course => new CourseDTO
            {
                CourseId = course.CourseId,
                CourseTitle = course.CourseTitle,
                Description = course.Description,
                TeacherId = course.TeacherId,
                CategoryName = categories.First(cate => cate.CategoryId == course.CategoryId).Name,
                CreateDate = course.CreateDate,
                Price = course.Price,
                ImageURL = course.ImageURL,
            });
        }

        public async Task<IEnumerable<CourseDTO>> SearchCoursesAsync(string keyword)
        {
            var courses = await _courseRepository.SearchCoursesAsync(keyword);
            return courses.Select(c => new CourseDTO
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                Description = c.Description,
                CategoryName = c.Category?.Name,
                Price = c.Price,
                Status = c.Status,
                ImageURL = c.ImageURL
            });
        }

        public async Task<bool> ApproveCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null || course.Status != Enum.CourseStatus.Pending)
            {
                return false;
            }
            return await _courseRepository.UpdateStatusAsync([id], Enum.CourseStatus.Approved);
        }

        public async Task<bool> UnApproveCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null || course.Status != Enum.CourseStatus.Approved)
            {
                return false;
            }
            return await _courseRepository.UpdateStatusAsync([id], Enum.CourseStatus.Pending);
        }
    }
}
