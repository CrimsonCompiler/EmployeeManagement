using Dapper;
using EmployeeManagement.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        // Helper Property
        private SqlConnection Connection => new SqlConnection(_connectionString);

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<Employee> GetAll()
        {
            using var connection = Connection;
            return connection.Query<Employee>("SELECT * FROM employees").ToList();
        }

        public Employee GetById(int id)
        {
            using var connection = Connection;
            return connection.QuerySingleOrDefault<Employee>("SELECT * FROM employees WHERE Id=@id", new { Id = id });
        }

        public int Create(Employee employee)
        {
            using var connection = Connection;
            string query = "INSERT INTO employees(Name, Department, Role) OUTPUT INSERTED.Id VALUES(@Name, @Department, @Role);";
            return connection.QuerySingle<int>(query, employee );
        }
        public bool Update(Employee employee)
        {
            using var connection = Connection;
            string query = "UPDATE employees SET Name=@Name, Department=@Department, Role=@Role WHERE Id=@Id;";
            return connection.Execute(query, employee) > 0;
        }

        public bool Delete(int id)
        {
            using var connection = Connection;
            string query = "DELETE FROM employees WHERE Id=@Id";
            return connection.Execute(query, new { Id = id }) > 0;
        }
    }
}
