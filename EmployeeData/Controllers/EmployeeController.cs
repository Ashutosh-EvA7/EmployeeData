using EmployeeAPI;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeData.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        Employee[] employees = new Employee[5];

        public EmployeeController()
        {
            // Store employee details in the array
            employees[0] = new Employee { Id = 1, Name = "Rajat", Position = "JSD", Salary = 35000, Dt = new DateTime(2024, 01, 25) };
            employees[1] = new Employee { Id = 2, Name = "Salony", Position = "JSD", Salary = 35000, Dt = new DateTime(2024, 02, 25) };
            employees[2] = new Employee { Id = 4, Name = "Manas", Position = "SSD", Salary = 50000, Dt = new DateTime(2024, 04, 25) };
            employees[3] = new Employee { Id = 5, Name = "Prajyot", Position = "CSO", Salary = 45000, Dt = new DateTime(2024, 05, 25) };
            employees[4] = new Employee { Id = 6, Name = "Rajat", Position = "CSO", Salary = 45000, Dt = new DateTime(2024, 06, 25) };
        }

        // GET api/employee
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        // GET api/employee/5
        public Employee Get(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        // POST api/employee
        public void Post([FromBody] Employee employee)
        {
            Employee[] newEmployees = new Employee[employees.Length + 1];
            Array.Copy(employees, newEmployees, employees.Length);
            newEmployees[newEmployees.Length - 1] = employee;
            employees = newEmployees;
        }

        // PUT api/employee/5
        public void Put(int id, [FromBody] Employee employee)
        {
            Employee empToUpdate = employees.FirstOrDefault(e => e.Id == id);
            if (empToUpdate != null)
            {
                empToUpdate.Name = employee.Name;
                empToUpdate.Position = employee.Position;
                empToUpdate.Salary = employee.Salary;
                empToUpdate.Dt = employee.Dt;
            }
        }

        // DELETE api/employee/5
        public void Delete(int id)
        {
            Employee[] newEmployees = new Employee[employees.Length - 1];
            int index = 0;
            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i].Id != id)
                {
                    newEmployees[index] = employees[i];
                    index++;
                }
            }
            employees = newEmployees;
        }

        [HttpGet]
        [Route("api/employee/secondhighestsalary")]
        public Employee GetSecondHighestSalary()
        {
            Array.Sort(employees, (x, y) => y.Salary.CompareTo(x.Salary));
            return employees[1];
        }

        [HttpGet]
        [Route("api/employee/datebetween/{startDate}")]
        public IEnumerable<Employee> GetEmployeesInDateRange(DateTime startDate, DateTime endDate)
        {
            return employees.Where(e => e.Dt >= startDate && e.Dt <= endDate);
        }

        [HttpPut]
        [Route("api/employee/updatefirstemployee")]
        public Employee UpdateFirstEmployee()
        {
            Array.Sort(employees, (x, y) => x.Salary.CompareTo(y.Salary));
            employees[0].Position = "Junior " + employees[0].Position;
            employees[0].Salary = 40000.00;
            return employees[0];
        }
    }
}
