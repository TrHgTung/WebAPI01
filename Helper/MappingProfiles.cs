using AutoMapper;
using WebAPI01.Dto;
using WebAPI01.Models;

namespace WebAPI01.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<TeacherDto, Teacher>();
            CreateMap<CourseDto, Course>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
