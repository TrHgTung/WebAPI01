namespace WebAPI01.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set; }

    }
}
