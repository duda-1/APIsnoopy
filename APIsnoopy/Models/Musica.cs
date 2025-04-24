using System.ComponentModel.DataAnnotations;

namespace APIsnoopy.Models;
public class Musica
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AlbumId { get; set; }
    public Guid ArtistaId { get; set; }
    public string Titulo { get; set; }
    public TimeSpan? Duracao { get; set; }
    public string ArquivoUrl { get; set; }
    public string Genero { get; set; }
}