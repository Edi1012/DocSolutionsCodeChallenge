﻿using DocSolutionsCodeChallenge.Models;
using DocSolutionsCodeChallenge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DocSolutionsCodeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // POST /employee
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                _employeeRepository.InsertEmployee(employee.Name, employee.User, employee.Password);
                return Ok("Employee created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating employee: {ex.Message}");
            }
        }

        // GET /employee
        [HttpGet]
        public IActionResult ListEmployees()
        {
            try
            {
                IEnumerable<Employee> employees = _employeeRepository.ListEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error listing employees: {ex.Message}");
            }
        }

        // POST /employee/Login
        [HttpPost("Login")]
        public IActionResult ValidateEmployeeLogin([FromBody] EmployeeLoginRequest request)
        {
            try
            {
                bool isValidLogin = _employeeRepository.ValidateEmployeeLogin(request.User, request.Password);
                if (isValidLogin)
                {
                    return Ok("Login successful");
                }
                else
                {
                    return Unauthorized("Login failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error validating employee login: {ex.Message}");
            }
        }
    }

    //public class Employee
    //{
    //    public string Name { get; set; }
    //    public string User { get; set; }
    //    public string Password { get; set; }
    //}

    public class EmployeeLoginRequest
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
