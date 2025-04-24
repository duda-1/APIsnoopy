using System.ComponentModel.DataAnnotations;

namespace APIsnoopy.Models;
public class Album
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ArtistaId { get; set; }
    public string Titulo { get; set; }
    public int? AnoLancamento { get; set; }
    public string CapaUrl { get; set; }
}