using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Logs;
using api.Models;
using Microsoft.AspNetCore.HttpOverrides;

namespace api.Mappers
{
    public static class LogMappers
    {
        public static LogDto ToLogDto(this Logger logModel)
        {
            return new LogDto
            {
                Id = logModel.Id,
                Name = logModel.Name,
                ReadData = logModel.ReadData,
                LoginDate = logModel.LoginDate
            };
        }

        public static Logger ToLogFromCreateDto(this CreateLogRequestDto logModelDto)
        {
            return new Logger
            {
                Name = logModelDto.Name,
                ReadData = logModelDto.ReadData,
                LoginDate = DateTime.Now,
                UserIp = logModelDto.UserIp
            };
        }
    }
}