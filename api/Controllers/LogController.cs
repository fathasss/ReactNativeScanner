using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Logs;
using api.Interfaces;
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
        private readonly ILogRepository _logRepo;

        public LogController(ApplicationDBContext context,ILogRepository logRepository)
        {
            _context = context;
            _logRepo = logRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _logRepo.GetAllAsync();
            var logDto = logs.Select(x => x.ToLogDto());
            return Ok(logDto);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var log = await _logRepo.GetByIdAsync(id);

            if (log == null)
                return NotFound();

            return Ok(log.ToLogDto());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateLogRequestDto logDto)
        {
            var logModel = logDto.ToLogFromCreateDto();
            await _logRepo.CreateAsync(logModel);
            return CreatedAtAction(nameof(GetById), new { id = logModel.Id }, logModel.ToLogDto());
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLogRequestDto updateLog)
        {
            var logModel = await _logRepo.UpdateAsync(id,updateLog);

            if (logModel == null)
                return NotFound();

            return Ok(logModel.ToLogDto());
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var logModel = await _logRepo.DeleteAsync(id);

            if(logModel == null)
                return NotFound();

            return NoContent();
        }
    }
}