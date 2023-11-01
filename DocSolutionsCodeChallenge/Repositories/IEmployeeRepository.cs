using DocSolutionsCodeChallenge.Models;

namespace DocSolutionsCodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        void InsertEmployee(string name, string user, string password);
        IEnumerable<Employee> ListEmployees();
        bool EmployeeLogin(string user, string password);
        bool UserEmployeeExist(string user);
    }
}