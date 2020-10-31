using juegos_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace juegos_mvc.Database
{
    public class PortalJuegosDbContext : DbContext
    {
        public PortalJuegosDbContext(DbContextOptions<PortalJuegosDbContext> options) : base(options)
        {
        }


        #region DbSets

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Consola> Consolas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Juego> Juegos { get; set; }
        public DbSet<JuegoGenero> JuegoGeneros { get; set; }

        #endregion
    }
}
