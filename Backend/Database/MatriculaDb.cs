using BancodeDados_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BancodeDados_Backend.Database
{
    public class MatriculaDb : DbContext
    {
        public MatriculaDb(DbContextOptions<MatriculaDb> options) : base(options) { }
        public DbSet<Matricula> Matriculas { get; set; }
    }
}