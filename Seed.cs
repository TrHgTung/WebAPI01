using System.Diagnostics.Metrics;
using WebAPI01.Data;
using WebAPI01.Models;

namespace SeedData
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.CourseTeachers.Any())
            {
                var courseTeachers = new List<CourseTeacher>()
                {
                    new CourseTeacher()
                    {
                        Course = new Course()
                        {
                            Name = "Lập trình căn bản",
                            BirthDate = new DateTime(2023,11,11),
                            CourseCategories = new List<CourseCategory>()
                            {
                                new CourseCategory { Category = new Category() { Name = "Công nghệ phần mềm"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Lập trình căn bản",Text = "Khóa hoc dễ hiểu", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Nguyễn", LastName = "Bình" } },
                                new Review { Title="Lập trình căn bản", Text = "Khóa hoc giúp tôi trở thành Hiếu PC", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Hiếu", LastName = "Thứ Hai" } },
                                new Review { Title="Lập trình căn bản",Text = "Chả hiểu gì cả", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Võ", LastName = "Minh" } },
                            }
                        },
                        Teacher = new Teacher()
                        {
                            FirstName = "Trần",
                            LastName = "Dần",
                            Degree = "ThS",
                            Department = new Department()
                            {
                                Name = "CNTT"
                            }
                        }
                    },
                    new CourseTeacher()
                    {
                        Course = new Course()
                        {
                            Name = "Cấu trúc dữ liệu",
                            BirthDate = new DateTime(2023,10,11),
                            CourseCategories = new List<CourseCategory>()
                            {
                                new CourseCategory { Category = new Category() { Name = "Khoa học máy tính"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Cấu trúc dữ liệu", Text = "Quá dễ dàng với tôi", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Hiếu", LastName = "Thứ Ba" } },
                                new Review { Title= "Cấu trúc dữ liệu",Text = "Không hiểu gì hết nhưng vẫn cho 5 sao", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "James", LastName = "Bond" } },
                                new Review { Title= "Cấu trúc dữ liệu", Text = "Khó hiểu quá", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Trương", LastName = "Ngọc" } },
                            }
                        },
                        Teacher = new Teacher()
                        {
                            FirstName = "Huấn",
                            LastName = "Hoa Hồng",
                            Degree = "ThS",
                            Department = new Department()
                            {
                                Name = "CNTT"
                            }
                        }
                    },
                                    new CourseTeacher()
                    {
                        Course = new Course()
                        {
                            Name = "Xác suất thống kê",
                            BirthDate = new DateTime(2023,11,11),
                            CourseCategories = new List<CourseCategory>()
                            {
                                new CourseCategory { Category = new Category() { Name = "Toán - Tin"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Xác suất thống kê",Text = "Thật dễ hiểu, đã giúp tôi không bị rớt môn", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Kenny", LastName = "Đặng" } },
                                new Review { Title="Xác suất thống kê",Text = "Oke dạy học dễ hiểu", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Hoàng", LastName = "Tùng" } },
                                new Review { Title="Xác suất thống kê",Text = "Khó mà không hiểu gì hết huhu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Bá", LastName = "Dũng" } },
                            }
                        },
                        Teacher = new Teacher()
                        {
                            FirstName = "Đoàn",
                            LastName = "Đức",
                            Degree = "TS",
                            Department = new Department()
                            {
                                Name = "Toán - Tin"
                            }
                        }
                    }
                };
                dataContext.CourseTeachers.AddRange(courseTeachers);
                dataContext.SaveChanges();
            }
        }
    }
}