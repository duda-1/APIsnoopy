using Microsoft.AspNetCore.Mvc;
using Supabase;
using APIsnoopy.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace APIsnoopy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase // Corrigido: herdando ControllerBase
    {
        private readonly Client _supabase;

        public LoginController(IConfiguration configuration)
        {
            var service = new SupabaseService(configuration);
            _supabase = service.GetClient();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest(new { error = "Nome e senha são obrigatórios." });
            }

            try
            {
                var nomeNormalizado = request.Nome.Trim().ToLower();

                // Procura o usuário pelo nome
                var usuariosEncontrados = await _supabase
                    .From<usuarios>()
                    .Where(u => u.Nome == nomeNormalizado)
                    .Get();

                var usuario = usuariosEncontrados.Models.FirstOrDefault();
                if (usuario == null)
                    return Unauthorized(new { error = "Usuário ou senha inválidos." });

                // Verifica a senha usando BCrypt
                bool senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha);

                if (!senhaValida)
                    return Unauthorized(new { error = "Usuário ou senha inválidos." });

                // Se tudo certo, retorna sucesso
                return Ok(new
                {
                    message = "Login realizado com sucesso!",
                    user = new
                    {
                        usuario.id,
                        usuario.Nome,
                        usuario.Email,
                        usuario.img_url
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro interno no servidor.", details = ex.Message });
            }
        }
    }

    // DTO para login
    public class LoginRequestDto
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
