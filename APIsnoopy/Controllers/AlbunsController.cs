using APIsnoopy.Models.Data;
using APIsnoopy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIsnoopy.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AlbunsController : ControllerBase
{
    private readonly AppDbContext _context;
    public AlbunsController(AppDbContext context) => _context = context;

    [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Albuns.ToListAsync());
    [HttpGet("{id}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _context.Albuns.FindAsync(id));
    [HttpPost] public async Task<IActionResult> Create(Album item) { _context.Albuns.Add(item); await _context.SaveChangesAsync(); return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); }
    [HttpPut("{id}")] public async Task<IActionResult> Update(Guid id, Album item) { if (id != item.Id) return BadRequest(); _context.Entry(item).State = EntityState.Modified; await _context.SaveChangesAsync(); return NoContent(); }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(Guid id) { var entity = await _context.Albuns.FindAsync(id); if (entity == null) return NotFound(); _context.Albuns.Remove(entity); await _context.SaveChangesAsync(); return NoContent(); }
}
