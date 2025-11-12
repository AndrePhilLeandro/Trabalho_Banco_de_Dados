using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class DisciplinaDb : DbContext
    {
        public DisciplinaDb(DbContextOptions<DisciplinaDb> options) : base(options) { }
        public DbSet<Disciplina> Disciplinas { get; set; }
    }
}