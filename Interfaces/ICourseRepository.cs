using WebAPI01.Dto;
using WebAPI01.Models;

namespace WebAPI01.Interfaces
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourses();
        Course GetCourse(int id);
        Course GetCourse(string name);
        Course GetCourseTrimToUpper(CourseDto courseCreate);
        decimal GetCourseRating(int courseId);
        bool CourseExists(int courseId);
        bool CreateCourse(int teacherId, int categoryId, Course course);
        bool UpdateCourse(int teacherId, int categoryId, Course course);
        bool DeleteCourse(Course course);
        bool Save();
    }
}
