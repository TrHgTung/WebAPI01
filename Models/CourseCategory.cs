namespace WebAPI01.Models
{
    public class CourseCategory
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public Course Course { get; set; }
        public Category Category { get; set; }
    }
}
