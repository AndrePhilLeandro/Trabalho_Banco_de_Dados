using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Data_nascimento { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
        public bool EhAluno { get; set; }

    }
    public class UsuarioLogin
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public bool EhAluno { get; set; }
    }
    public class UsuarioPut
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
    }
}