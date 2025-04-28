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

        public UsuarioController(IConfiguration configuration)
        {
            var service = new SupabaseService(configuration);
            _supabase = service.GetClient();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest(new { error = "Todos os campos obrigat�rios (Nome, Email e Senha) devem ser preenchidos." });
            }

            try
            {
                var nomeNormalizado = request.Nome.Trim().ToLower();
                var emailNormalizado = request.Email.Trim().ToLower();

                var existingEmail = await _supabase.From<usuarios>().Where(u => u.Email == emailNormalizado).Get();
                if (existingEmail.Models.Any())
                    return BadRequest(new { error = "E-mail j� cadastrado." });

                var existingNome = await _supabase.From<usuarios>().Where(u => u.Nome == nomeNormalizado).Get();
                if (existingNome.Models.Any())
                    return BadRequest(new { error = "Nome de usu�rio j� est� em uso." });

                var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

                var newUser = new usuarios
                {
                    Nome = nomeNormalizado,
                    Email = emailNormalizado,
                    Senha = senhaHash,
                    img_url = request.ImgUrl
                };

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

        // M�TODO PUT PARA ATUALIZAR USU�RIO
        [HttpPut("{nome}")]
        public async Task<IActionResult> UpdateUser(string nome, [FromBody] UpdateUserRequestDto request)
        {
            try
            {
                // Normaliza o nome antes da busca
                var nomeNormalizado = nome.Trim().ToLower();

                var usuariosEncontrados = await _supabase
                    .From<usuarios>()
                    .Where(u => u.Nome == nomeNormalizado) // Agora � simples, sem m�todos dentro do Where
                    .Get();

                var usuario = usuariosEncontrados.Models.FirstOrDefault();

                if (usuario == null)
                {
                    return NotFound(new { error = "Usu�rio n�o encontrado." });
                }

                // Atualizar apenas se o campo vier preenchido
                if (!string.IsNullOrWhiteSpace(request.Nome))
                    usuario.Nome = request.Nome.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(request.Email))
                    usuario.Email = request.Email.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(request.Senha))
                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);

                if (!string.IsNullOrWhiteSpace(request.ImgUrl))
                    usuario.img_url = request.ImgUrl;

                var updateResult = await _supabase.From<usuarios>().Update(usuario);

                if (updateResult.Models.Any())
                {
                    return Ok(new
                    {
                        message = "Usu�rio atualizado com sucesso!",
                        user = new
                        {
                            usuario.id,
                            usuario.Nome,
                            usuario.Email,
                            usuario.img_url
                        }
                    });
                }
                else
                {
                    return StatusCode(500, new { error = "Erro ao atualizar usu�rio." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro interno no servidor.", details = ex.Message });
            }
        }



        public class RegisterRequestDto
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public string ImgUrl { get; set; }
        }

        public class UpdateUserRequestDto
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public string ImgUrl { get; set; }
        }
    }
}
