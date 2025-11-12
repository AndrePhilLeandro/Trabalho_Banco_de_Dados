using BancodeDados_Backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BancodeDados_Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BancodeDados_Backend.Controller
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoDb alunoDb;
        public string token { get; set; }
        private readonly PasswordHasher<string> hasher = new PasswordHasher<string>();
        public AlunoController(AlunoDb alunoDb, PasswordHasher<string> hasher)
        {
            this.alunoDb = alunoDb;
            this.hasher = hasher;
        }
        [AllowAnonymous]
        [HttpPost("Cadastro_Alunos")]
        public IActionResult Cadastro_Alunos([FromBody] Models.Aluno alunotmp)
        {
            var VerificaAluno = alunoDb.Alunos.FirstOrDefault(al => al.Email == alunotmp.Email || al.Cpf == alunotmp.Cpf);
            if (VerificaAluno != null)
            {
                return Conflict("Aluno ja cadastrado!");
            }
            if (alunotmp.Senha.Length < 8)
            {
                return UnprocessableEntity("A senha precisa ter mais que 8 Caracteres!");
            }
            var Senha = HashSenha(alunotmp.Senha);
            alunotmp.Senha = Senha;
            alunoDb.Alunos.Add(alunotmp);
            alunoDb.SaveChanges();
            return Created($"Aluno {alunotmp.Nome} Criado", "");
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] AlunoLogin alunotmp)
        {
            var VerificaLogin = alunoDb.Alunos.FirstOrDefault(al => al.Email == alunotmp.Email);
            var Verifica = VerificaSenha(VerificaLogin.Senha, alunotmp.Senha);
            if (VerificaLogin.Email == alunotmp.Email && Verifica == true)
            {
                var chave = "Projeto_Banco_de_Dados_Puc_Minas_2025";
                var chaveEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
                var credenciais = new SigningCredentials(chaveEncriptada, SecurityAlgorithms.Aes128CbcHmacSha256);
                var claims = new List<Claim>();
                claims.Add(new Claim("email", VerificaLogin.Email));
                claims.Add(new Claim("Id", VerificaLogin.Id_aluno.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, "Aluno"));
                var JWT = new JwtSecurityToken(
                issuer: "Nome do Projeto",
                audience: "Usuario",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credenciais,
                claims: claims
                );
                token = new JwtSecurityTokenHandler().WriteToken(JWT);
            }
            return Ok(new
            {
                Token = token
            });
        }
        [Authorize]
        [HttpPut("{Id}")]
        public IActionResult Editar_Aluno(int id, [FromBody] AlunoPut alunotmp)
        {
            if (id <= 0)
            {
                return BadRequest("Id Invalido!");
            }
            var VerificaAluno = alunoDb.Alunos.FirstOrDefault(user => user.Id_aluno == id);
            if (VerificaAluno == null)
            {
                return NotFound("Aluno não encontrado!");
            }
            var senhahash = HashSenha(alunotmp.Senha);
            VerificaAluno.Nome = alunotmp.Nome;
            VerificaAluno.Data_nascimento = alunotmp.Data_nascimento;
            VerificaAluno.Email = alunotmp.Email;
            VerificaAluno.Telefone = alunotmp.Telefone;
            VerificaAluno.Senha = senhahash;
            alunoDb.SaveChanges();
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