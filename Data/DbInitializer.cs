using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //Procurar por linhas na tabela
            if (context.Tags.Any())
            {
                return;
            }

            var tags = new Tag[]
            {
                new Tag
                {
                    Nome = "TI",
                    Cor = "3D3D3D"
                },
                new Tag
                {
                    Nome = "Engenharia",
                    Cor = "7F7F7F"
                },
                new Tag
                {
                    Nome = "Geologia",
                    Cor = "C4C4C4"
                }
            };
            

            context.AddRange(tags);
            context.SaveChanges();
        }

        public static void AddDefaultUser(UserManager<IdentityUser> userManager)
        {
            //Criar usuário de teste
            var user = new IdentityUser { UserName = "eitufmt" };
            userManager.CreateAsync(user, "eitufmt");
        }
    }
}