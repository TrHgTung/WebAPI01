using AutoMapper;
using WebAPI01.Data;
using WebAPI01.Interfaces;
using WebAPI01.Models;

namespace WebAPI01.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool DepartmentExists(int id)
        {
            return _context.Departments.Any(c => c.Id == id);
        }

        public bool CreateDepartment(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public ICollection<Department> GetDepartments()
        {
           return _context.Departments.ToList();
        }

        public Department GetDepartment(int id)
        {
            return _context.Departments.Where(c => c.Id == id).FirstOrDefault();
        }

        public Department GetDepartmentByTeacher(int teacherId)
        {
            return _context.Teachers.Where(o => o.Id == teacherId).Select(c => c.Department).FirstOrDefault();
        }

        public ICollection<Teacher> GetTeachersFromADepartment(int departmentId)
        {
            return _context.Teachers.Where(c => c.Department.Id == departmentId).ToList(); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDepartment(Department department)
        {
            _context.Update(department);
            return Save();
        }
    }
}
