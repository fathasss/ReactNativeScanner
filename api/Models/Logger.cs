using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Logger
    {
        public int Id { get; set; }

        public string Name { get; set;} = string.Empty;

        public string ReadData {get;set;} = string.Empty;

        public DateTime LoginDate {get;set;} = DateTime.Now;

        public string UserIp { get; set; } = string.Empty;
    }
}