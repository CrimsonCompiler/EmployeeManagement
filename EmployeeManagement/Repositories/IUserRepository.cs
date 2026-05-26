using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public interface IUserRepository
    {
        User GetUserCredentials(string username, string password);
    }
}
