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
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorDb professorDb;
        public string token{ get; set; }
        private readonly PasswordHasher<string> hasher = new PasswordHasher<string>();
        public ProfessorController(ProfessorDb professorDb, PasswordHasher<string> hasher)
        {
            this.professorDb = professorDb;
            this.hasher = hasher;
        }
        [AllowAnonymous]
        [HttpPost("Cadastro_Professor")]
        public IActionResult Cadastro_Professor([FromBody] Professor profe)
        {
            var VerificaLogin = professorDb.Professores.FirstOrDefault(prof => prof.Email == profe.Email);
            if (VerificaLogin != null)
            {
                return Conflict("Professor ja Cadastrado!");
            }
            if (profe.Senha.Length < 8)
            {
                return UnprocessableEntity("Senha minima precisa ter 8 Caracteres");
            }
            var senha = HashSenha(profe.Senha);
            profe.Senha = senha;
            professorDb.Professores.Add(profe);
            professorDb.SaveChanges();
            return Created($"Professor {profe.Nome} Criado com sucesso!", "");
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] ProfessorLogin profe)
        {
            var VerificaLogin = professorDb.Professores.FirstOrDefault(prof => prof.Email == profe.Email);
            var senha = VerificaSenha(VerificaLogin.Senha, profe.Senha);
            if (VerificaLogin.Email == profe.Email && senha == true)
            {
                var chave = "Projeto_Banco_de_Dados_Puc_Minas_2025";
                var chaveEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
                var credenciais = new SigningCredentials(chaveEncriptada, SecurityAlgorithms.Aes128CbcHmacSha256);
                var claims = new List<Claim>();
                claims.Add(new Claim("email", VerificaLogin.Email));
                claims.Add(new Claim("Id", VerificaLogin.Id_professor.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, "Professor"));
                var JWT = new JwtSecurityToken(
                issuer: "Nome do Projeto",
                audience: "Usuario",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credenciais,
                claims: claims
                );
                token = new JwtSecurityTokenHandler().WriteToken(JWT);
            }
            return Ok();
        }
        [Authorize]
        [HttpPut("{Id}")]
        public IActionResult EditaProfessor(int id, [FromBody] Professortmp profe)
        {
            if (id <= 0)
            {
                return BadRequest("Id Invalido!");
            }
            var VerificaLogin = professorDb.Professores.FirstOrDefault(prof => prof.Id_professor == id);
            if (VerificaLogin == null)
            {
                return NotFound("Professor nao Localizado!");
            }
            var senha = HashSenha(profe.Nome);
            VerificaLogin.Nome = profe.Nome;
            VerificaLogin.Email = profe.Email;
            VerificaLogin.Senha = senha;
            VerificaLogin.Telefone = profe.Telefone;
            professorDb.SaveChanges();
            return NoContent();
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