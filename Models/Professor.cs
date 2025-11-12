using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Professor
    {
        [Key]
        public int Id_professor { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
    }
    public class ProfessorLogin
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
    public class Professortmp
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
    }
}