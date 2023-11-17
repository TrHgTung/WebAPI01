namespace WebAPI01.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public Department Department { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; }
    }
}
