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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EmpreendedorismoEIT.Models.Empresa> Empresas { get; set; }
        public DbSet<EmpreendedorismoEIT.Models.Tag> Tags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relação de muitos para muitos (join table)
            modelBuilder.Entity<EmpresaTag>().HasKey(et => new { et.EmpresaID, et.TagID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
