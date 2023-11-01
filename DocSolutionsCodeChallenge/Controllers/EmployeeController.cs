using DocSolutionsCodeChallenge.Models;
using DocSolutionsCodeChallenge.Repositories;
using DocSolutionsCodeChallenge.Services;
using DocSolutionsCodeChallenge.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DocSolutionsCodeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtService _jwtService;

        public EmployeeController(IEmployeeRepository employeeRepository, IJwtService jwtService)
        {
            _employeeRepository = employeeRepository;
            _jwtService = jwtService;
        }
            
        // POST /employee
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDTO employee)
        {
            try
            {
                // Create an instance of the CreateEmployeeValidator and validate the employee object.
                var validator = new CreateEmployeeValidator(_employeeRepository);
                ValidationResult validationResult = validator.Validate(employee);

                if (!validationResult.IsValid)
                {
                    // Validation failed, return a BadRequest response with validation errors.
                    var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                    return BadRequest($"Validation failed: {string.Join(", ", errorMessages)}");
                }


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
        public IActionResult ValidateEmployeeLogin([FromBody] EmployeeLoginRequestDTO request)
        {
            try
            {
                bool isValidLogin = _employeeRepository.EmployeeLogin(request.User, request.Password);
                if (isValidLogin)
                {
                    // Generate a JWT token using the injected JWT service
                    var token = _jwtService.GenerateToken(request.User);

                    // Return the token as part of a response
                    return Ok(new { Token = token });
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

    public class EmployeeDTO
    {
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class EmployeeLoginRequestDTO
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
