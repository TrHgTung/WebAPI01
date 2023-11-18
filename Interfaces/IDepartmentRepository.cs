using WebAPI01.Models;

namespace WebAPI01.Interfaces
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();
        Department GetDepartment(int id);
        Department GetDepartmentByTeacher(int teacherId);
        ICollection<Teacher> GetTeachersFromADepartment(int departmentId);
        bool DepartmentExists(int id);
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        bool Save();
    }
}
