using System.ComponentModel.DataAnnotations;

namespace BancodeDados_Backend.Models
{
    public class Frequencia
    {
        [Key]
        public int Id_frequencia { get; set; }
        public int Presenca { get; set; }
        public int Faltas { get; set; }
        public int Id_matriculaFK{ get; set; }
    }
}