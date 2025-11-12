using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Aluno
    {
        [Key]
        public int Id_aluno { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Data_nascimento { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }

    }
    public class AlunoLogin
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
    public class AlunoPut
    {
        public string? Nome { get; set; }
        public string? Data_nascimento { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
    }
}