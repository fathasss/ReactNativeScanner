using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Logs
{
    public class LoginEmployeeDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}