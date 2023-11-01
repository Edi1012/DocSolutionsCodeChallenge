using FluentValidation;
using DocSolutionsCodeChallenge.Models;
using DocSolutionsCodeChallenge.Repositories;

namespace DocSolutionsCodeChallenge.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(employee => employee.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NotNull().WithMessage("Name cannot be null.");

            RuleFor(employee => employee.User)
                .NotEmpty().WithMessage("User is required.")
                .NotNull().WithMessage("User cannot be null.");

            RuleFor(employee => employee.Password)
                .NotEmpty().WithMessage("Password is required.")
                .NotNull().WithMessage("Password cannot be null.");

            RuleFor(employee => employee.User)
                .Must(user => !UserExists(user)).WithMessage("User already exists.");
        }

        private bool UserExists(string user)
        {
            var employee = _employeeRepository.UserEmployeeExist(user);
            return employee;
        }
    }
}
