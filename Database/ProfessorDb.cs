using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class ProfessorDb : DbContext
    {
        public ProfessorDb(DbContextOptions<ProfessorDb> options) : base(options) { }
        public DbSet<Professor> Professores{ get; set; }
    }
}