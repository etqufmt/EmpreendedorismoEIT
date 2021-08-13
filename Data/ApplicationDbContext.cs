using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EmpreendedorismoEIT.Models;

namespace EmpreendedorismoEIT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EmpreendedorismoEIT.Models.DadosIncubada> DadosIncubadas { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.DadosJunior> DadosJuniores { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.Dashboard> Dashboard { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.Empresa> Empresas { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.EmpresaTag> EmpresaTags { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.ProdServico> ProdServicos { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.RamoAtuacao> RamosAtuacao { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.RedeSocial> RedesSociais { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relação de muitos para muitos (join table)
            modelBuilder.Entity<EmpresaTag>().HasKey(et => new { et.EmpresaID, et.TagID });
            //Utiliza classe apenas para receber resultados de consultas
            modelBuilder.Entity<Dashboard>().ToTable("Dashboard", t => t.ExcludeFromMigrations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
