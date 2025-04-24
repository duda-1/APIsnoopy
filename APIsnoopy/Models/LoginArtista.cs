using System.ComponentModel.DataAnnotations;

namespace APIsnoopy.Models;
public class LoginArtista
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string SenhaHash { get; set; }
}