using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Matricula
    {
        [Key]
        public int Id_matricula { get; set; }
        public int Id_alunoFK { get; set; }
        public int Id_turmaFK { get; set; }
        public string? Data_matricula { get; set; }
    }
}