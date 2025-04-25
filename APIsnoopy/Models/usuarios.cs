using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;
namespace APIsnoopy.Models
{
    [Table("usuario")]
    public class usuarios: BaseModel
    {
        [PrimaryKey("id", false)]
        [JsonIgnore]
        public Guid id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }

        [Column("E-mail")]
        public string Email { get; set; }

        [Column("senha")]
        public string Senha { get; set; }
        [Column("artista")]
        public bool artista {  get; set; }
    }
}
