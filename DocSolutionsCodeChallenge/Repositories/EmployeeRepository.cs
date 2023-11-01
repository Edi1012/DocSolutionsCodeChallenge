using DocSolutionsCodeChallenge.Models;
using System.Data;
using System.Data.SqlClient;

namespace DocSolutionsCodeChallenge.Repositories


{


    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString;

        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertEmployee(string name, string user, string password)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("InsertEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@password", password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) 
            { }

        }

        public IEnumerable<Employee> ListEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("ListEmployees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["name"].ToString(),
                                User = reader["user"].ToString(),
                                Password = reader["password"].ToString()
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

        public bool ValidateEmployeeLogin(string user, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("EmployeeLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@inputUser", user);
                    command.Parameters.AddWithValue("@inputPassword", password);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) == 1;
                }
            }
        }
    }

}
