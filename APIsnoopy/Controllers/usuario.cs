using APIsnoopy.Models;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIsnoopy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly Client _supabase;

        // Construtor para inicializar o cliente Supabase
        public UsuarioController(IConfiguration configuration)
        {
            var service = new SupabaseService(configuration);
            _supabase = service.GetClient(); // Certifique-se de que o m�todo GetClient() retorna o cliente corretamente
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Validar os campos obrigat�rios
            if (string.IsNullOrWhiteSpace(request.Nome) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest(new { error = "Todos os campos obrigat�rios (Nome, Email e Senha) devem ser preenchidos." });
            }

            try
            {
                // Normalizar o nome e email
                var nomeNormalizado = request.Nome.Trim().ToLower();
                var emailNormalizado = request.Email.Trim().ToLower();

                // Verificar se o email j� est� cadastrado
                var existingEmail = await _supabase.From<usuarios>().Where(u => u.Email == emailNormalizado).Get();
                if (existingEmail.Models.Any())
                    return BadRequest(new { error = "E-mail j� cadastrado." });

                // Verificar se o nome de usu�rio j� est� em uso
                var existingNome = await _supabase.From<usuarios>().Where(u => u.Nome == nomeNormalizado).Get();
                if (existingNome.Models.Any())
                    return BadRequest(new { error = "Nome de usu�rio j� est� em uso." });

                // Hash da senha
                var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

                // Criar o novo usu�rio
                var newUser = new usuarios
                {
                    Nome = nomeNormalizado,
                    Email = emailNormalizado,
                    Senha = senhaHash,
                    img_url = request.ImgUrl // Substitu�do o campo "Artista" por "ImgUrl"
                };

                // Inserir o novo usu�rio no banco
                var result = await _supabase.From<usuarios>().Insert(newUser);

                if (result.Models.Any())
                {
                    return Ok(new
                    {
                        message = "Usu�rio cadastrado com sucesso!",
                        user = new
                        {
                            newUser.Nome,
                            newUser.Email,
                            newUser.img_url,
                        }
                    });
                }
                else
                {
                    return StatusCode(500, new { error = "Erro ao cadastrar usu�rio." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro interno no servidor.", details = ex.Message });
            }
        }
    }

    // Classe DTO usada na requisi��o para registro
    public class RegisterRequestDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ImgUrl { get; set; } // Substitu�do "Artista" por "ImgUrl"
    }
}
