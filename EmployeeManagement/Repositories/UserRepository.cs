using Dapper;
using EmployeeManagement.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Repositories
{

    public class UserRepository : IUserRepository
    {

        private readonly string _connectionString;

        // Helper Property
        private SqlConnection Connection => new SqlConnection(_connectionString);

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public User GetUserCredentials(string username, string password)
        {
            string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
            return Connection.QuerySingleOrDefault<User>(
                query,
                new { Username = username, Password = password });
        }
    }
}
