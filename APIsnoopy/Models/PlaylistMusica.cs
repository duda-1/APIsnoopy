using System.ComponentModel.DataAnnotations;

namespace APIsnoopy.Models;
public class PlaylistMusica
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlaylistId { get; set; }
    public Guid MusicaId { get; set; }
    public int Ordem { get; set; }
}