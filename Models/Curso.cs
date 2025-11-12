using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Curso
    {
        [Key]
        public int Id_curso { get; set; }
        public string? Nome { get; set; }
        public int Carga_horaria { get; set; }
    }
}