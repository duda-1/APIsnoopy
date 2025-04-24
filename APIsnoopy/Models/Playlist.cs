using System.ComponentModel.DataAnnotations;

namespace APIsnoopy.Models;
public class Playlist
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public DateTime CriadaEm { get; set; } = DateTime.Now;
}
