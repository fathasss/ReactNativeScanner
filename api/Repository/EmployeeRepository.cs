using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helper;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;

        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }
    }
}