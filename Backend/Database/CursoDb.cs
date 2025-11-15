using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class CursoDb : DbContext
    {
        public CursoDb(DbContextOptions<CursoDb> options) : base(options) { }
        public DbSet<Curso> Cursos { get; set; }
    }
}