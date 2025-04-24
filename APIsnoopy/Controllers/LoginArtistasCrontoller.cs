using APIsnoopy.Models.Data;
using APIsnoopy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIsnoopy.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LoginArtistasController : ControllerBase
{
    private readonly AppDbContext _context;
    public LoginArtistasController(AppDbContext context) => _context = context;

    [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.LoginArtistas.ToListAsync());
    [HttpGet("{id}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _context.LoginArtistas.FindAsync(id));
    [HttpPost] public async Task<IActionResult> Create(LoginArtista item) { _context.LoginArtistas.Add(item); await _context.SaveChangesAsync(); return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); }
    [HttpPut("{id}")] public async Task<IActionResult> Update(Guid id, LoginArtista item) { if (id != item.Id) return BadRequest(); _context.Entry(item).State = EntityState.Modified; await _context.SaveChangesAsync(); return NoContent(); }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(Guid id) { var entity = await _context.LoginArtistas.FindAsync(id); if (entity == null) return NotFound(); _context.LoginArtistas.Remove(entity); await _context.SaveChangesAsync(); return NoContent(); }
}