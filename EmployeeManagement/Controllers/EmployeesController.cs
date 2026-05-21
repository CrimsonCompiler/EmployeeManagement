using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly string _connectionString;

        public EmployeesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/employees
        [HttpGet]
        public IActionResult GetAll()
        {
            //return Ok(_employees); // 200 OK with JSON DATA

            string query = "SELECT * FROM employees";

            using(var connection = new SqlConnection(_connectionString))
            {
                var employees = connection.Query<Employee>(query).ToList();

                return Ok(employees);
            }

        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            string query = "SELECT * FROM employees WHERE Id = @id";
            using (var connection = new SqlConnection(_connectionString))
            {

                var employee = connection.Query<Employee>(query, new { id });

                if(employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
        }


        // POST: api/employees
        [HttpPost]
        public IActionResult Create([FromBody] Employee newEmp)
        {
            string query = @"
                              INSERT INTO employees(Name, Department, Role) 
                              OUTPUT INSERTED.Id
                              VALUES(@Name, @Department, @Role);
                             ";

            using (var connection = new SqlConnection(_connectionString))
            {
                int insertedId = connection.QuerySingle<int>(query, newEmp);

                newEmp.Id = insertedId;

                return CreatedAtAction(nameof(GetAll), new { id = newEmp.Id }, newEmp);
            }
        }


        // PUT: api/employees/{id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee updatedEmp)
        {
            string query = @"
                            UPDATE employees
                            SET Name = @Name, Department = @Department, Role = @Role
                            WHERE Id = @Id
                            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                updatedEmp.Id = id;

                int rowsAffected = connection.Execute(query, updatedEmp);

                if(rowsAffected == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }


        //// DELETE: /api/employees/{id}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var emp = _employees.FirstOrDefault(e => e.Id == id);
        //    if(emp == null)
        //    {
        //        return NotFound();
        //    }

        //    _employees.Remove(emp);
        //    return NoContent();
        //}
    }
}
