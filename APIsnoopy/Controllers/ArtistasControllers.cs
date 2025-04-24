using APIsnoopy.Models.Data;
using APIsnoopy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIsnoopy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ArtistasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _context.Artistas.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var artista = await _context.Artistas.FindAsync(id);
        if (artista == null) return NotFound();
        return Ok(artista);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Artista artista)
    {
        _context.Artistas.Add(artista);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = artista.Id }, artista);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Artista updated)
    {
        if (id != updated.Id) return BadRequest();
        _context.Entry(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var artista = await _context.Artistas.FindAsync(id);
        if (artista == null) return NotFound();
        _context.Artistas.Remove(artista);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
