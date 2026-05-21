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

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var existing_employee = _employees.FirstOrDefault(e => e.Id == id);

            if (existing_employee == null)
            {
                return NotFound();
            }

            return Ok(existing_employee);
        }


        // POST: api/employees
        [HttpPost]
        public IActionResult Create([FromBody] Employee newEmp)
        {
            newEmp.Id = _employees.Count + 1;
            _employees.Add(newEmp);
            return CreatedAtAction(nameof(GetAll), new { id = newEmp.Id }, newEmp);
        }


        // PUT: api/employees/{id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee updatedEmp)
        {
            var existing_emp = _employees.FirstOrDefault(e => e.Id == id);

            if(existing_emp == null)
            {
                return NotFound();
            }

            // Found and update
            existing_emp.Name = updatedEmp.Name;
            existing_emp.Department = updatedEmp.Department;
            existing_emp.Role = updatedEmp.Role;

            return NoContent();
        }

    }
}
