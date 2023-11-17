namespace WebAPI01.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set;}
    }
}
