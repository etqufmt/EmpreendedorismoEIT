﻿using EmpreendedorismoEIT.Models;
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
                new RamoAtuacao { CNAE = "A (01-03)",Denominacao = "AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA",},
                new RamoAtuacao { CNAE = "B (05-09)",Denominacao = "INDÚSTRIAS EXTRATIVAS",},
                new RamoAtuacao { CNAE = "C (10-33)",Denominacao = "INDÚSTRIAS DE TRANSFORMAÇÃO",},
                new RamoAtuacao { CNAE = "D (35-35)",Denominacao = "ELETRICIDADE E GÁS",},
                new RamoAtuacao { CNAE = "E (36-39)",Denominacao = "ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO",},
                new RamoAtuacao { CNAE = "F (41-43)",Denominacao = "CONSTRUÇÃO",},
                new RamoAtuacao { CNAE = "G (45-47)",Denominacao = "COMÉRCIO E REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS",},
                new RamoAtuacao { CNAE = "H (49-53)",Denominacao = "TRANSPORTE, ARMAZENAGEM E CORREIO",},
                new RamoAtuacao { CNAE = "I (55-56)",Denominacao = "ALOJAMENTO E ALIMENTAÇÃO",},
                new RamoAtuacao { CNAE = "J (58-63)",Denominacao = "INFORMAÇÃO E COMUNICAÇÃO",},
                new RamoAtuacao { CNAE = "K (64-66)",Denominacao = "ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS",},
                new RamoAtuacao { CNAE = "L (68-68)",Denominacao = "ATIVIDADES IMOBILIÁRIAS",},
                new RamoAtuacao { CNAE = "M (69-75)",Denominacao = "ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS",},
                new RamoAtuacao { CNAE = "N (77-82)",Denominacao = "ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES",},
                new RamoAtuacao { CNAE = "O (84-84)",Denominacao = "ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL",},
                new RamoAtuacao { CNAE = "P (85-85)",Denominacao = "EDUCAÇÃO",},
                new RamoAtuacao { CNAE = "Q (86-88)",Denominacao = "SAÚDE HUMANA E SERVIÇOS SOCIAIS",},
                new RamoAtuacao { CNAE = "R (90-93)",Denominacao = "ARTES, CULTURA, ESPORTE E RECREAÇÃO",},
                new RamoAtuacao { CNAE = "S (94-96)",Denominacao = "OUTRAS ATIVIDADES DE SERVIÇOS",},
                new RamoAtuacao { CNAE = "T (97-97)",Denominacao = "SERVIÇOS DOMÉSTICOS",},
                new RamoAtuacao { CNAE = "U (99-99)",Denominacao = "ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS",},
            };



            //var tags = new Tag[]
            //{
            //    new Tag
            //    {
            //        Nome = "TI",
            //        Cor = "B676B1"
            //    },
            //    new Tag
            //    {
            //        Nome = "Engenharia",
            //        Cor = "82CAAF"
            //    },
            //    new Tag
            //    {
            //        Nome = "Geologia",
            //        Cor = "75C0E0"
            //    },
            //    new Tag
            //    {
            //        Nome = "Agricultura",
            //        Cor = "FECE67"
            //    },
            //    new Tag
            //    {
            //        Nome = "Saneamento",
            //        Cor = "FFAA80"
            //    },
            //    new Tag
            //    {
            //        Nome = "Consultoria",
            //        Cor = "9999FF"
            //    },
            //    new Tag
            //    {
            //        Nome = "Construção Civil",
            //        Cor = "42BD88"
            //    },
            //    new Tag
            //    {
            //        Nome = "Robótica",
            //        Cor = "999966"
            //    },
            //    new Tag
            //    {
            //        Nome = "Pecuária",
            //        Cor = "EE778F"
            //    },
            //};

            //var empJuniores = new Empresa[]
            //{
            //    new Empresa
            //    {
            //        Nome = "Fácil Consultoria",
            //        Tipo = Tipo.JUNIOR,
            //        DescricaoCurta = "Desde 1993, facilitando a realização dos seus sonhos!",
            //        DescricaoLonga = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
            //        Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
            //        Telefone = "6532323232",
            //        Email = "ej.facilconsultoria@gmail.com",
            //        Situacao = Situacao.ATIVA,
            //        UltimaModificacao = DateTime.Now,
            //        DadosJunior = new DadosJunior
            //        {
            //            Campus = Campus.CUIABA,
            //            Instituto = "FACC"
            //        },
            //        ProdutosServicos = new List<ProdutoServico>
            //        {
            //            new ProdutoServico
            //            {
            //                Nome = "Plano de negócios",
            //                Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
            //            },
            //            new ProdutoServico
            //            {
            //                Nome = "Estudo de viabilidade econômica",
            //                Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
            //            },
            //            new ProdutoServico
            //            {
            //                Nome = "Plano de marketing",
            //                Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
            //            },
            //        },
            //        RedesSociais = new List<RedeSocial>
            //        {
            //            new RedeSocial
            //            {
            //                Plataforma = Plataforma.WEBSITE,
            //                URL = "https://ufmt.br"
            //            },
            //            new RedeSocial
            //            {
            //                Plataforma = Plataforma.FACEBOOK,
            //                URL = "https://pt-br.facebook.com/ufmatogrosso"
            //            },
            //            new RedeSocial
            //            {
            //                Plataforma = Plataforma.INSTAGRAM,
            //                URL = "https://www.instagram.com/ufmt.br"
            //            },
            //            new RedeSocial
            //            {
            //                Plataforma = Plataforma.TWITTER,
            //                URL = "https://twitter.com/UFMT"
            //            },
            //        }
            //    },
            //    new Empresa
            //    {
            //        Nome = "Nubank",
            //        Tipo = Tipo.JUNIOR,
            //        DescricaoCurta = "Empresa startup brasileira pioneira no segmento de serviços financeiros.",
            //        DescricaoLonga = "A primeira compra realizada com um cartão Nubank ocorreu em 1º de abril de 2014. Em 2018, o Nubank atingiu o status de startup unicórnio ao atingir avaliação de preço de mercado no valor de 1 bilhão de dólares, sendo a terceira empresa brasileira com esta marca até então.",
            //        Endereco = "R. Capote Valente, 39 - Pinheiros, São Paulo - SP, 05409-000",
            //        Telefone = "1132323232",
            //        Email = "nubank@nubank.com.br",
            //        Situacao = Situacao.ATIVA,
            //        UltimaModificacao = DateTime.Now,
            //        DadosJunior = new DadosJunior
            //        {
            //            Campus = Campus.ARAGUAIA,
            //            Instituto = "Faculdade de Economia"
            //        },
            //        ProdutosServicos = new List<ProdutoServico>
            //        {
            //            new ProdutoServico
            //            {
            //                Nome = "Teste #01"
            //            },
            //            new ProdutoServico
            //            {
            //                Nome = "Teste #02"
            //            },
            //            new ProdutoServico
            //            {
            //                Nome = "Teste #03"
            //            }
            //        }
            //    },
            //    new Empresa
            //    {
            //        Nome = "EBANX",
            //        Tipo = Tipo.JUNIOR,
            //        DescricaoCurta = "Fintech brasileira fundada em 2012 que oferece soluções de pagamento.",
            //        DescricaoLonga = "Em maio de 2021, a fintech anunciou uma série de mudanças em sua alta liderança. João Del Valle, até então COO, passou a assumir o cargo de CEO, posto ocupado durante 9 anos por Alphonse Voigt. Voigt passa a responder como presidente-executivo, liderando o conselho administrativo da empresa.",
            //        Endereco = "R. Mal. Deodoro, 630 - Centro, Curitiba - PR, 80010-010",
            //        Telefone = "5487878787",
            //        Email = "ebanx@business.ebanx.com",
            //        Situacao = Situacao.INATIVA,
            //        UltimaModificacao = DateTime.Now,
            //        DadosJunior = new DadosJunior
            //        {
            //            Campus = Campus.SINOP,
            //            Instituto = "FAET"
            //        }
            //    },
            //    new Empresa
            //    {
            //        Nome = "Quinto Andar",
            //        Tipo = Tipo.JUNIOR,
            //        DescricaoCurta = "Startup brasileira de tecnologia focada no aluguel e na venda de imóveis.",
            //        DescricaoLonga = "Na modalidade venda, a empresa permite (através de aplicativo) a negociação direta entre comprador e vendedor, e cuida a partir daí de todo o processo, inclusive diligência sobre a propriedade e viabilização de financiamento bancário.",
            //        Endereco = "R. Girassol, 555 - Vila Madalena, São Paulo - SP, 05433-001",
            //        Telefone = "11989898989",
            //        Email = "email@quintoandar.com.br",
            //        Situacao = Situacao.ATIVA,
            //        UltimaModificacao = DateTime.Now,
            //        DadosJunior = new DadosJunior
            //        {
            //            Campus = Campus.VGRANDE,
            //            Instituto = "Engenharia"
            //        }
            //    },
            //};

            context.AddRange(ramosAtuacao);
            //context.AddRange(empJuniores);
            //context.AddRange(tags);
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