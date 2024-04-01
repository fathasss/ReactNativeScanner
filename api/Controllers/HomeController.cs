using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Logs;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var logs = _context.Log.ToList()
                .Select(x => x.ToLogDto());
            return Ok(logs);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var log = _context.Log.Find(id);

            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateLogRequestDto logDto)
        {
            var logModel = logDto.ToLogFromCreateDto();
            _context.Log.Add(logModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = logModel.Id }, logModel.ToLogDto());
        }
    }
}