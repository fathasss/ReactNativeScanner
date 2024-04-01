using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Logs
{
    public class LogDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ReadData { get; set; } = string.Empty;

        public DateTime LoginDate { get; set; } = DateTime.Now;
    }
}