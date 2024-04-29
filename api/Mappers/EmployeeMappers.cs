using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Logs;
using api.Models;

namespace api.Mappers
{
    public class EmployeeMappers
    {
        public static Employee ToEmployee(string email, string password)
        {
            return new Employee()
            {
                Email = email,
                Password = password
            };
        }
    }
}