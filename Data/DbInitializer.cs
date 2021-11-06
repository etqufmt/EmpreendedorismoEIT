using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, ILogger<Program> logger)
        {
            var ramosAtuacao = new RamoAtuacao[]
            {
                new RamoAtuacao { CNAE = "01-03 AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA" },
                new RamoAtuacao { CNAE = "05-09 INDÚSTRIAS EXTRATIVAS" },
                new RamoAtuacao { CNAE = "10-33 INDÚSTRIAS DE TRANSFORMAÇÃO" },
                new RamoAtuacao { CNAE = "35-35 ELETRICIDADE E GÁS" },
                new RamoAtuacao { CNAE = "36-39 ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO" },
                new RamoAtuacao { CNAE = "41-43 CONSTRUÇÃO" },
                new RamoAtuacao { CNAE = "45-47 COMÉRCIO E REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS" },
                new RamoAtuacao { CNAE = "49-53 TRANSPORTE, ARMAZENAGEM E CORREIO" },
                new RamoAtuacao { CNAE = "55-56 ALOJAMENTO E ALIMENTAÇÃO" },
                new RamoAtuacao { CNAE = "58-63 INFORMAÇÃO E COMUNICAÇÃO" },
                new RamoAtuacao { CNAE = "64-66 ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS" },
                new RamoAtuacao { CNAE = "68-68 ATIVIDADES IMOBILIÁRIAS" },
                new RamoAtuacao { CNAE = "69-75 ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS" },
                new RamoAtuacao { CNAE = "77-82 ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES" },
                new RamoAtuacao { CNAE = "84-84 ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL" },
                new RamoAtuacao { CNAE = "85-85 EDUCAÇÃO" },
                new RamoAtuacao { CNAE = "86-88 SAÚDE HUMANA E SERVIÇOS SOCIAIS" },
                new RamoAtuacao { CNAE = "90-93 ARTES, CULTURA, ESPORTE E RECREAÇÃO" },
                new RamoAtuacao { CNAE = "94-96 OUTRAS ATIVIDADES DE SERVIÇOS" },
                new RamoAtuacao { CNAE = "97-97 SERVIÇOS DOMÉSTICOS" },
                new RamoAtuacao { CNAE = "99-99 ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS" },
            };

            var tags = new Tag[]
            {
                new Tag { Nome = "Administração", Cor = 0xFECE67 },
                new Tag { Nome = "Agricultura", Cor = 0x999966 },
                new Tag { Nome = "Arquitetura", Cor = 0x75C0E0 },
                new Tag { Nome = "Automação", Cor = 0xB676B1 },
                new Tag { Nome = "Contabilidade", Cor = 0xEE778F },
                new Tag { Nome = "Construção Civil", Cor = 0x42BD88 },
                new Tag { Nome = "Consultoria", Cor = 0xFFAA80 },
                new Tag { Nome = "Engenharia", Cor = 0x9999FF },
                new Tag { Nome = "Eventos", Cor = 0x82CAAF },
                new Tag { Nome = "Financeiro", Cor = 0xFECE67 },
                new Tag { Nome = "Florestal", Cor = 0x999966 },
                new Tag { Nome = "Geologia", Cor = 0x75C0E0 },
                new Tag { Nome = "Indústria", Cor = 0xB676B1 },
                new Tag { Nome = "Logística", Cor = 0xEE778F },
                new Tag { Nome = "Marketing", Cor = 0x42BD88 },
                new Tag { Nome = "Meio Ambiente", Cor = 0xFFAA80 },
                new Tag { Nome = "Pecuária", Cor = 0x9999FF },
                new Tag { Nome = "Saneamento", Cor = 0x82CAAF },
                new Tag { Nome = "Tecnologia da Informação", Cor = 0xFECE67 },
                new Tag { Nome = "Vendas", Cor = 0x999966 },
            };

            context.AddRange(ramosAtuacao);
            context.AddRange(tags);
            context.SaveChanges();
            logger.LogInformation("[DEBUG] Banco de dados populado");
        }

        public static async Task AddDefaultUserAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<Program> logger)
        {
            var result = await userManager.FindByNameAsync("eitufmt");
            if (result == null)
            {
                //Cria níveis de autorização
                await roleManager.CreateAsync(new IdentityRole("admin"));
                await roleManager.CreateAsync(new IdentityRole("nonadmin"));
                
                //Cria usuário de teste
                var user = new IdentityUser { UserName = "eitufmt" };
                await userManager.CreateAsync(user, "eitufmt");
                await userManager.AddToRoleAsync(user, "admin");
                logger.LogInformation("[DEBUG] Usuário padrão criado");
            }
        }
    }
}