using DocSolutionsCodeChallenge.Models;

namespace DocSolutionsCodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        void InsertEmployee(string name, string user, string password);
        IEnumerable<Employee> ListEmployees();
        bool ValidateEmployeeLogin(string user, string password);
    }
}