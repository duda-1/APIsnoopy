using System;
using System.ComponentModel.DataAnnotations;
namespace APIsnoopy.Models;
public class Artista
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string Nacionalidade { get; set; }
    public string GeneroArtistico { get; set; }
    public string Biografia { get; set; }
    public string FotoUrl { get; set; }
}