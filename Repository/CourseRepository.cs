using WebAPI01.Data;
using WebAPI01.Dto;
using WebAPI01.Interfaces;
using WebAPI01.Models;

namespace WebAPI01.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context) {
            _context = context;
        }

        public bool CreateCourse(int teacherId, int categoryId, Course course)
        {
            var courseTeacherEntity = _context.Teachers.Where(a => a.Id == teacherId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var courseTeacher = new CourseTeacher()
            {
                Teacher = courseTeacherEntity,
                Course = course,
            };

            _context.Add(courseTeacher);

            var courseCategory = new CourseCategory()
            {
                Category = category,
                Course = course,
            };
            _context.Add(courseCategory);
            _context.Add(course);

            return Save();
        }

        public bool DeleteCourse(Course course)
        {
            _context.Remove(course);
            return Save();
        }

        public Course GetCourse(int id)
        {
            return _context.Course.Where(p => p.Id == id).FirstOrDefault();
        }

        public Course GetCourse(string name)
        {
            return _context.Course.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetCourseRating(int courseId)
        {
            var review = _context.Reviews.Where(p => p.Course.Id == courseId);
            if (review.Count() <= 0) {
                return 0;
            }
            else
            {
                return (decimal)review.Sum(r => r.Rating) / review.Count();
            }
        }

        public ICollection<Course> GetCourses()
        {
            return _context.Course.OrderBy(p => p.Id).ToList();
        }

        public Course GetCourseTrimToUpper(CourseDto courseCreate)
        {
            return GetCourses().Where(c => c.Name.Trim().ToUpper() == courseCreate.Name.TrimEnd().ToUpper())
               .FirstOrDefault();
        }

        public bool CourseExists(int courseId)
        {
            return _context.Course.Any(p => p.Id == courseId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCourse(int teacherId, int categoryId, Course course)
        {
            _context.Update(course);
            return Save();
        }
    }
}
