using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _repository = employeeRepository;
        }


        // GET: api/employees
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) { NotFound(); }
            return Ok(employee);
        }


        // POST: api/employees
        [HttpPost]
        public IActionResult Create([FromBody] Employee newEmp)
        {
            var newId = _repository.Create(newEmp);
            newEmp.Id = newId;
            return CreatedAtAction(nameof(GetById), new {Id = newId}, newEmp);
        }


        // PUT: api/employees/{id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee updatedEmp)
        {
            updatedEmp.Id = id;
            if (!_repository.Update(updatedEmp)) return NotFound();
            return NoContent();
        }


        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           if(!_repository.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
