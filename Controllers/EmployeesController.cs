using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.WebAPI.Data;
using EmployeeManager.WebAPI.Models;
using EmployeeManager.WebAPI.Models.Dtos;
using EmployeeManager.WebAPI.Repositories;

namespace EmployeeManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeGetDto>>> GetEmployees()
        {
            var allEmployees = await _employeeRepository.GetAll()
                .Include(e => e.Country)
                .Include(e => e.JobCategory)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<EmployeeGetDto>>(allEmployees));
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGetDto>> GetEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetAll()
                    .Include(e => e.Country)
                    .Include(e => e.JobCategory)
                    .FirstOrDefaultAsync(e=>e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeGetDto>(employee));
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public IActionResult PutEmployee(Guid id, EmployeePutPostDto employeeDto)
        {
            var employee =  _employeeRepository.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            _mapper.Map(employeeDto, employee);

            try
            {
                _employeeRepository.Update(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated");
        }

        // POST: api/Employees
        [HttpPost]
        public ActionResult<EmployeePutPostDto> PostEmployee(EmployeePutPostDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _employeeRepository.Insert(employee);

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, _mapper.Map<EmployeeGetDto>(employee));
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<EmployeePutPostDto> DeleteEmployee(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.Delete(id);

            return Ok("Deleted");
        }

        private bool EmployeeExists(Guid id)
        {
            return _employeeRepository.Exists(id);
        }
    }
}
