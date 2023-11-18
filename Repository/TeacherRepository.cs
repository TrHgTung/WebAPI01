using WebAPI01.Data;
using WebAPI01.Interfaces;
using WebAPI01.Models;
using System.Diagnostics.Metrics;

namespace WebAPI01.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTeacher(Teacher teacher)
        {
           _context.Add(teacher);
            return Save();
        }

        public bool DeleteTeacher(Teacher teacher)
        {
            _context.Remove(teacher);
            return Save();
        }

        public Teacher GetTeacher(int teacherId)
        {
            return _context.Teachers.Where(o => o.Id == teacherId).FirstOrDefault();
        }

        public ICollection<Teacher> GetTeacherOfACourse(int courseId)
        {
            return _context.CourseTeachers.Where(p => p.Course.Id == courseId).Select(o => o.Teacher).ToList();
        }

        public ICollection<Teacher> GetTeachers()
        {
           return _context.Teachers.ToList();
        }

        public ICollection<Course> GetCourseByTeacher(int teacherId)
        {
           return _context.CourseTeachers.Where(p => p.Course.Id == teacherId).Select(p =>p.Course).ToList();
        }

        public bool TeacherExists(int teacherId)
        {
            return _context.Teachers.Any(o => o.Id == teacherId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();  
            return saved > 0 ? true : false;
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            _context.Update(teacher);
            return Save();
        }
    }
}
