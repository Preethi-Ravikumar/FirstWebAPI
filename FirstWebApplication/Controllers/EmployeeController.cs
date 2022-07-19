using FirstWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var employees = new List<Employee>();
            using (var context = new EmployeeDBContext())
            {
                employees = context.Employees.ToList();
            }
            return employees;
        }
        [HttpDelete]
        public string DeleteEmployee(int employeeId)
        {
            try
            {
                using (var context = new EmployeeDBContext())
                {
                    var employee = context.Employees.Where(e => e.EmpId == employeeId).SingleOrDefault();
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                }
                return "Record with Id=" + employeeId + " is deleted successfully";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }
        }
        [HttpPost]
        public string InsertEmployee(Employee employee)
        {
            try
            {
                using (var context = new EmployeeDBContext())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
                return "Record inserted successfully";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }
        }
        [HttpPut]
        public string UpdateEmployee(Employee employee)
        {
            try
            {
                using (var context = new EmployeeDBContext())
                {
                    var emp = context.Employees.Where(e => e.EmpId == employee.EmpId).SingleOrDefault();
                    emp.EmpId = employee.EmpId;
                    emp.EmpName = employee.EmpName;
                    emp.Location = employee.Location;
                    emp.Email = employee.Email;
                    emp.Phone = employee.Phone;
                    context.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                return "Record with Id=" + employee.EmpId + " is updated successfully";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }

        }

    }
}
