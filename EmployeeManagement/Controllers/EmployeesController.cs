using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
       {
           new Employee{Id = 1, Name = "Tousif", Department="Engineering", Role="Backend Dev"},
           new Employee{Id = 2, Name = "Karim", Department="HR", Role="Manager"}
       };

        // GET: api/employees
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_employees); // 200 OK with JSON DATA
        }

        // POST: api/employees
        public IActionResult Create([FromBody] Employee newEmp)
        {
            newEmp.Id = _employees.Count + 1;
            _employees.Add(newEmp);
            return CreatedAtAction(nameof(GetAll), new { id = newEmp.Id }, newEmp);
        } 

    }
}
