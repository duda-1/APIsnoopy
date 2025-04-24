using APIsnoopy.Models.Data;
using APIsnoopy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIsnoopy.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LoginUsuariosController : ControllerBase
{
    private readonly AppDbContext _context;
    public LoginUsuariosController(AppDbContext context) => _context = context;

    [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.LoginUsuarios.ToListAsync());
    [HttpGet("{id}")] public async Task<IActionResult> GetById(Guid id) => Ok(await _context.LoginUsuarios.FindAsync(id));
    [HttpPost] public async Task<IActionResult> Create(LoginUsuario item) { _context.LoginUsuarios.Add(item); await _context.SaveChangesAsync(); return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); }
    [HttpPut("{id}")] public async Task<IActionResult> Update(Guid id, LoginUsuario item) { if (id != item.Id) return BadRequest(); _context.Entry(item).State = EntityState.Modified; await _context.SaveChangesAsync(); return NoContent(); }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(Guid id) { var entity = await _context.LoginUsuarios.FindAsync(id); if (entity == null) return NotFound(); _context.LoginUsuarios.Remove(entity); await _context.SaveChangesAsync(); return NoContent(); }
}