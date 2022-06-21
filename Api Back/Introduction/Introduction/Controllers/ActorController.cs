using Introduction.DAL;
using Introduction.Dtos;
using Introduction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _context.Actors.ToListAsync());
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int id)
        {
            Actor actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null) return BadRequest();
            return Ok(actor);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(CreateDto actorDto)
        {
            if (actorDto == null) return BadRequest();
            Actor actor = new Actor
            {
                Name = actorDto.Name,
                Image = actorDto.Image,
                IsDeleted = false
            };
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
            return Ok(actorDto);
        }

        [HttpPut("")]
        public async Task<IActionResult> Update(int id, UpdateDto actorDto)
        {
            if (actorDto == null) return BadRequest();
            Actor actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null) return NotFound();
            actor.Name = actorDto.Name;
            actor.Image = actorDto.Image;
            await _context.SaveChangesAsync();
            return Ok(actorDto);
        }

        [HttpDelete("")]
        public IActionResult Delete(int id)
        {
            Actor actor = _context.Actors.Find(id);
            if (actor == null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
