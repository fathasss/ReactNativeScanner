using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Logs;
using api.Models;

namespace api.Interfaces
{
    public interface ILogRepository
    {
        Task<List<Logger>> GetAllAsync();
        
        Task<Logger?> GetByIdAsync(int id);

        Task<Logger> CreateAsync(Logger logModel);

        Task<Logger?> UpdateAsync(int id, UpdateLogRequestDto logDto);
        
        Task<Logger?> DeleteAsync(int id);        
    }
}