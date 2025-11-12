using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class TurmaDb : DbContext
    {
        public TurmaDb(DbContextOptions<TurmaDb> options) : base(options) { }
        public DbSet<Turma> Turmas { get; set; }
    }
}