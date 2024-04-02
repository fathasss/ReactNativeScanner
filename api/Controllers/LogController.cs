using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Logs;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LogController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _context.Log.ToListAsync();
            var logDto = logs.Select(x => x.ToLogDto());
            return Ok(logDto);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var log = await _context.Log.FindAsync(id);

            if (log == null)
                return NotFound();

            return Ok(log.ToLogDto());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateLogRequestDto logDto)
        {
            var logModel = logDto.ToLogFromCreateDto();
            await _context.Log.AddAsync(logModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = logModel.Id }, logModel.ToLogDto());
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLogRequestDto updateLog)
        {
            var logModel = await _context.Log.FirstOrDefaultAsync(x => x.Id == id);

            if (logModel == null)
                return NotFound();

            logModel.Name = updateLog.Name;
            logModel.ReadData = updateLog.ReadData;
            logModel.UserIp = updateLog.UserIp;

            await _context.SaveChangesAsync();

            return Ok(logModel.ToLogDto());
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var logModel = await _context.Log.FirstOrDefaultAsync(x=>x.Id == id);

            if(logModel == null)
                return NotFound();

            _context.Log.Remove(logModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}