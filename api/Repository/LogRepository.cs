using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
        public Task<List<Logger>> GetAllAsync()
        {
            return _context.Log.ToListAsync(); 
        }
    }
}