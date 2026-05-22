using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        int Create(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
    }
}
