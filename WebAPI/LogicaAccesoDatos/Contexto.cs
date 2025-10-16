using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesNegocio;
using LogicaNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<EnvioComun> EnviosComunes { get; set; }
        public DbSet<EnvioUrgente> EnviosUrgentes { get; set; }
        public DbSet<Agencia> Agencias { get; set; }

        public Contexto(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Envio>().HasOne(e => e.Cliente).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Envio>().HasOne(e => e.Empleado).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comentario>().HasOne(c => c.Empleado).WithMany().OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
    }
}
