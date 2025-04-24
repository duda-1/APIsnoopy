using APIsnoopy.Models.Data;
using APIsnoopy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIsnoopy.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MusicasController : ControllerBase
{
    private readonly AppDbContext _context;
    public MusicasController(AppDbContext context) => _context = context;

    [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Musicas.ToListAsync());
    [HttpGet("{id}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _context.Musicas.FindAsync(id));
    [HttpPost] public async Task<IActionResult> Create(Musica item) { _context.Musicas.Add(item); await _context.SaveChangesAsync(); return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); }
    [HttpPut("{id}")] public async Task<IActionResult> Update(Guid id, Musica item) { if (id != item.Id) return BadRequest(); _context.Entry(item).State = EntityState.Modified; await _context.SaveChangesAsync(); return NoContent(); }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(Guid id) { var entity = await _context.Musicas.FindAsync(id); if (entity == null) return NotFound(); _context.Musicas.Remove(entity); await _context.SaveChangesAsync(); return NoContent(); }
}

