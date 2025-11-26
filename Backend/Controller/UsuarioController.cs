using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BancodeDados_Backend.Database;
using BancodeDados_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BancodeDados_Backend.Controller
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDb usuarioDb;
        private readonly PasswordHasher<string> hasher;

        public UsuarioController(UsuarioDb usuarioDb, PasswordHasher<string> hasher)
        {
            this.usuarioDb = usuarioDb;
            this.hasher = hasher;
        }

        [AllowAnonymous]
        [HttpPost("CadastroUser")]
        public IActionResult CadastroUser([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Dados inválidos.");

            if (string.IsNullOrWhiteSpace(usuario.Nome))
                return UnprocessableEntity("O nome não pode estar em branco.");

            usuario.Nome = usuario.Nome.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(usuario.Cpf))
            {
                return UnprocessableEntity("O CPF é obrigatório.");
            }
            usuario.Cpf = usuario.Cpf.Trim();
            var achaUsuario = usuarioDb.Usuarios.FirstOrDefault(u => u.Cpf == usuario.Cpf);

            if (achaUsuario != null && achaUsuario.Email == usuario.Email && achaUsuario.Cpf == usuario.Cpf)
                return Conflict("Usuário já cadastrado!");

            if (string.IsNullOrWhiteSpace(usuario.Senha) || usuario.Senha.Length < 8)
                return BadRequest("A senha precisa ter pelo menos 8 caracteres!");

            usuario.Senha = HashSenha(usuario.Senha);

            usuarioDb.Usuarios.Add(usuario);
            usuarioDb.SaveChanges();

            return Created("Usuario", "Criado com sucesso!");
        }
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(UsuarioLogin usuario)
        {
            if (usuario.Email == "Administrador@master.com" && usuario.Senha == "123456" && usuario.Tipo == 0)
            {
                return Ok(new
                {
                    login = usuario,
                    nome = "Admin"
                });
            }
            string audiencia = "Admin";
            string? tokenGerado = null;

            var VerificaLogin = usuarioDb.Usuarios.FirstOrDefault(al => al.Email == usuario.Email);

            if (VerificaLogin == null)
            {
                return Unauthorized("Usuário não encontrado");
            }
            if (VerificaLogin.Ativo == false)
            {
                return Unauthorized("Usuario desativado!");
            }

            var Verifica = VerificaSenha(VerificaLogin.Senha, usuario.Senha);

            if (VerificaLogin.Email == usuario.Email && Verifica == true && VerificaLogin.Tipo == usuario.Tipo)
            {
                var chave = "Projeto_Banco_de_Dados_Puc_Minas_2025";
                var chaveEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
                var credenciais = new SigningCredentials(chaveEncriptada, SecurityAlgorithms.Aes128CbcHmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim("email", VerificaLogin.Email));
                claims.Add(new Claim("Id", VerificaLogin.Id.ToString()));

                if (VerificaLogin.Tipo == 1)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Aluno"));
                    audiencia = "Aluno";
                }
                else if (VerificaLogin.Tipo == 2)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Professor"));
                    audiencia = "Professor";
                }
                var JWT = new JwtSecurityToken(
                    issuer: "Nome do Projeto",
                    audience: audiencia,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credenciais,
                    claims: claims
                );
                tokenGerado = new JwtSecurityTokenHandler().WriteToken(JWT);
            }
            else
            {
                return Unauthorized("Email ou senha inválidos");
            }
            return Ok(new
            {
                tokenGerado,
                login = VerificaLogin
            }
            );
        }
        [HttpPut("{id}")]
        public IActionResult Edita_Usuario(int id, UsuarioPut usuario)
        {
            Console.WriteLine($"{usuario.Email}\n{usuario.Nome}\n{usuario.Senha}\n{usuario.Telefone}\n {id}");
            if (id <= 0)
            {
                return BadRequest("Id Invalido!");
            }
            var VerificaLogin = usuarioDb.Usuarios.FirstOrDefault(al => al.Id == id);
            if (VerificaLogin == null)
            {
                return NotFound();
            }
            if (VerificaLogin.Id != id)
            {
                return NotFound();
            }
            if (string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.Telefone))
            {
                return BadRequest("Nehum dos campos pode estar Vazio!");
            }
            if (usuario.Telefone.Length < 11)
            {
                return BadRequest("Telefone Invalido!");
            }
            if (VerificaLogin.Ativo == true)
            {
                var HashPassword = HashSenha(usuario.Senha);
                VerificaLogin.Nome = usuario.Nome.ToUpper();
                VerificaLogin.Email = usuario.Email;
                VerificaLogin.Senha = HashPassword;
                VerificaLogin.Telefone = usuario.Telefone;
                usuarioDb.SaveChanges();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Deleta_Usuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id Invalido!");
            }
            var VerificaLogin = usuarioDb.Usuarios.FirstOrDefault(al => al.Id == id);
            if (VerificaLogin == null)
            {
                return NotFound();
            }
            if (VerificaLogin.Id != id)
            {
                return NotFound();
            }
            if (VerificaLogin.Ativo == true)
            {
                VerificaLogin.Ativo = false;
                usuarioDb.SaveChanges();
            }
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult Ativa(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id Invalido!");
            }
            var VerificaLogin = usuarioDb.Usuarios.FirstOrDefault(al => al.Id == id);
            if (VerificaLogin == null)
            {
                return NotFound();
            }
            if (VerificaLogin.Id != id)
            {
                return NotFound();
            }
            if (VerificaLogin.Ativo == false)
            {
                VerificaLogin.Ativo = true;
                usuarioDb.SaveChanges();
            }
            return NoContent();
        }
        [HttpGet("MostrarTodos")]
        public IActionResult MostraTodos()
        {

            var verifica = usuarioDb.Usuarios.ToList();
            return Ok(verifica);
        }

        [HttpGet("{id}")]
        public IActionResult MostraUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id Invalido!");
            }
            var VerificaLogin = usuarioDb.Usuarios.FirstOrDefault(al => al.Id == id);
            if (VerificaLogin == null)
            {
                return NotFound();
            }
            if (VerificaLogin.Id != id)
            {
                return NotFound();
            }
            return Ok(VerificaLogin);
        }

        private bool VerificaSenha(string hashBanco, string hashLogin)
        {
            var resultado = hasher.VerifyHashedPassword(null, hashBanco, hashLogin);
            var result = hasher.VerifyHashedPassword(null, hashBanco, hashLogin);
            return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
        private string HashSenha(string senha)
        {
            return hasher.HashPassword(null, senha);
        }
    }
}