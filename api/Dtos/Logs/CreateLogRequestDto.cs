using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Logs
{
    public class CreateLogRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string ReadData { get; set; } = string.Empty;
        public string UserIp { get; set; } = string.Empty;

    }
}