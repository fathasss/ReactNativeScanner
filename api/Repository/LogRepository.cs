using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Logs;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDBContext _context;


        public LogRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Logger>> GetAllAsync()
        {
            return await _context.Log.ToListAsync(); 
        }

        public async Task<Logger> CreateAsync(Logger logModel)
        {
            await _context.Log.AddAsync(logModel);
            await _context.SaveChangesAsync();
            return logModel;
        }

        public async Task<Logger?> DeleteAsync(int id)
        {
            var logModel = await _context.Log.FindAsync(id);

            if(logModel == null)
                return null;

            _context.Log.Remove(logModel);
            await _context.SaveChangesAsync();
            return logModel;
        }

        public async Task<Logger?> GetByIdAsync(int id)
        {
            return await _context.Log.FindAsync(id);
        }

        public async Task<Logger?> UpdateAsync(int id, UpdateLogRequestDto logDto)
        {
            var existingLog = await _context.Log.FirstOrDefaultAsync(x=>x.Id == id);

            if(existingLog == null)
                return null;

            existingLog.Name = logDto.Name;
            existingLog.ReadData = logDto.ReadData;
            existingLog.UserIp = logDto.UserIp;

            await _context.SaveChangesAsync();

            return existingLog;
        }
    }
}