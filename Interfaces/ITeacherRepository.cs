using WebAPI01.Models;

namespace WebAPI01.Interfaces
{
    public interface ITeacherRepository
    {
        ICollection<Teacher> GetTeachers();
        Teacher GetTeacher(int teacherId);
        ICollection<Teacher> GetTeacherOfACourse(int courseId);
        ICollection<Course> GetCourseByTeacher(int teacherId);
        bool TeacherExists(int teacherId);
        bool CreateTeacher(Teacher teacher);
        bool UpdateTeacher(Teacher teacher);
        bool DeleteTeacher(Teacher teacher);
        bool Save();

    }
}
