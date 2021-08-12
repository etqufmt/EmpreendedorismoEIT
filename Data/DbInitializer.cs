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
        public static void Initialize(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            ILogger<Program> logger)
        {
            //Preencher tabela de ramos de atuação a partir de um CSV extraído do IBGE
            var ramosAtuacao = lerListaCNAE(webHostEnvironment.WebRootPath);

            //var ramosAtuacao = new RamoAtuacao[]
            //{
            //    new RamoAtuacao { CodigoCNAE = "A-01",Denominacao = "AGRICULTURA, PECUÁRIA E SERVIÇOS RELACIONADOS",},
            //    new RamoAtuacao { CodigoCNAE = "A-02",Denominacao = "PRODUÇÃO FLORESTAL",},
            //    new RamoAtuacao { CodigoCNAE = "A-03",Denominacao = "PESCA E AQUICULTURA",},
            //    new RamoAtuacao { CodigoCNAE = "B-05",Denominacao = "EXTRAÇÃO DE CARVÃO MINERAL",},
            //    new RamoAtuacao { CodigoCNAE = "B-06",Denominacao = "EXTRAÇÃO DE PETRÓLEO E GÁS NATURAL",},
            //    new RamoAtuacao { CodigoCNAE = "B-07",Denominacao = "EXTRAÇÃO DE MINERAIS METÁLICOS",},
            //    new RamoAtuacao { CodigoCNAE = "B-08",Denominacao = "EXTRAÇÃO DE MINERAIS NÃO METÁLICOS",},
            //    new RamoAtuacao { CodigoCNAE = "B-09",Denominacao = "ATIVIDADES DE APOIO À EXTRAÇÃO DE MINERAIS",},
            //    new RamoAtuacao { CodigoCNAE = "C-10",Denominacao = "FABRICAÇÃO DE PRODUTOS ALIMENTÍCIOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-11",Denominacao = "FABRICAÇÃO DE BEBIDAS",},
            //    new RamoAtuacao { CodigoCNAE = "C-12",Denominacao = "FABRICAÇÃO DE PRODUTOS DO FUMO",},
            //    new RamoAtuacao { CodigoCNAE = "C-13",Denominacao = "FABRICAÇÃO DE PRODUTOS TÊXTEIS",},
            //    new RamoAtuacao { CodigoCNAE = "C-14",Denominacao = "CONFECÇÃO DE ARTIGOS DO VESTUÁRIO E ACESSÓRIOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-15",Denominacao = "PREPARAÇÃO DE COUROS E FABRICAÇÃO DE ARTEFATOS DE COURO, ARTIGOS PARA VIAGEM E CALÇADOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-16",Denominacao = "FABRICAÇÃO DE PRODUTOS DE MADEIRA",},
            //    new RamoAtuacao { CodigoCNAE = "C-17",Denominacao = "FABRICAÇÃO DE CELULOSE, PAPEL E PRODUTOS DE PAPEL",},
            //    new RamoAtuacao { CodigoCNAE = "C-18",Denominacao = "IMPRESSÃO E REPRODUÇÃO DE GRAVAÇÕES",},
            //    new RamoAtuacao { CodigoCNAE = "C-19",Denominacao = "FABRICAÇÃO DE COQUE, DE PRODUTOS DERIVADOS DO PETRÓLEO E DE BIOCOMBUSTÍVEIS",},
            //    new RamoAtuacao { CodigoCNAE = "C-20",Denominacao = "FABRICAÇÃO DE PRODUTOS QUÍMICOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-21",Denominacao = "FABRICAÇÃO DE PRODUTOS FARMOQUÍMICOS E FARMACÊUTICOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-22",Denominacao = "FABRICAÇÃO DE PRODUTOS DE BORRACHA E DE MATERIAL PLÁSTICO",},
            //    new RamoAtuacao { CodigoCNAE = "C-23",Denominacao = "FABRICAÇÃO DE PRODUTOS DE MINERAIS NÃO METÁLICOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-24",Denominacao = "METALURGIA",},
            //    new RamoAtuacao { CodigoCNAE = "C-25",Denominacao = "FABRICAÇÃO DE PRODUTOS DE METAL, EXCETO MÁQUINAS E EQUIPAMENTOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-26",Denominacao = "FABRICAÇÃO DE EQUIPAMENTOS DE INFORMÁTICA, PRODUTOS ELETRÔNICOS E ÓPTICOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-27",Denominacao = "FABRICAÇÃO DE MÁQUINAS, APARELHOS E MATERIAIS ELÉTRICOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-28",Denominacao = "FABRICAÇÃO DE MÁQUINAS E EQUIPAMENTOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-29",Denominacao = "FABRICAÇÃO DE VEÍCULOS AUTOMOTORES, REBOQUES E CARROCERIAS",},
            //    new RamoAtuacao { CodigoCNAE = "C-30",Denominacao = "FABRICAÇÃO DE OUTROS EQUIPAMENTOS DE TRANSPORTE, EXCETO VEÍCULOS AUTOMOTORES",},
            //    new RamoAtuacao { CodigoCNAE = "C-31",Denominacao = "FABRICAÇÃO DE MÓVEIS",},
            //    new RamoAtuacao { CodigoCNAE = "C-32",Denominacao = "FABRICAÇÃO DE PRODUTOS DIVERSOS",},
            //    new RamoAtuacao { CodigoCNAE = "C-33",Denominacao = "MANUTENÇÃO, REPARAÇÃO E INSTALAÇÃO DE MÁQUINAS E EQUIPAMENTOS",},
            //    new RamoAtuacao { CodigoCNAE = "D-35",Denominacao = "ELETRICIDADE, GÁS E OUTRAS UTILIDADES",},
            //    new RamoAtuacao { CodigoCNAE = "E-36",Denominacao = "CAPTAÇÃO, TRATAMENTO E DISTRIBUIÇÃO DE ÁGUA",},
            //    new RamoAtuacao { CodigoCNAE = "E-37",Denominacao = "ESGOTO E ATIVIDADES RELACIONADAS",},
            //    new RamoAtuacao { CodigoCNAE = "E-38",Denominacao = "COLETA, TRATAMENTO E DISPOSIÇÃO DE RESÍDUOS; RECUPERAÇÃO DE MATERIAIS",},
            //    new RamoAtuacao { CodigoCNAE = "E-39",Denominacao = "DESCONTAMINAÇÃO E OUTROS SERVIÇOS DE GESTÃO DE RESÍDUOS",},
            //    new RamoAtuacao { CodigoCNAE = "F-41",Denominacao = "CONSTRUÇÃO DE EDIFÍCIOS",},
            //    new RamoAtuacao { CodigoCNAE = "F-42",Denominacao = "OBRAS DE INFRAESTRUTURA",},
            //    new RamoAtuacao { CodigoCNAE = "F-43",Denominacao = "SERVIÇOS ESPECIALIZADOS PARA CONSTRUÇÃO",},
            //    new RamoAtuacao { CodigoCNAE = "G-45",Denominacao = "COMÉRCIO E REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS",},
            //    new RamoAtuacao { CodigoCNAE = "G-46",Denominacao = "COMÉRCIO POR ATACADO, EXCETO VEÍCULOS AUTOMOTORES E MOTOCICLETAS",},
            //    new RamoAtuacao { CodigoCNAE = "G-47",Denominacao = "COMÉRCIO VAREJISTA",},
            //    new RamoAtuacao { CodigoCNAE = "H-49",Denominacao = "TRANSPORTE TERRESTRE",},
            //    new RamoAtuacao { CodigoCNAE = "H-50",Denominacao = "TRANSPORTE AQUAVIÁRIO",},
            //    new RamoAtuacao { CodigoCNAE = "H-51",Denominacao = "TRANSPORTE AÉREO",},
            //    new RamoAtuacao { CodigoCNAE = "H-52",Denominacao = "ARMAZENAMENTO E ATIVIDADES AUXILIARES DOS TRANSPORTES",},
            //    new RamoAtuacao { CodigoCNAE = "H-53",Denominacao = "CORREIO E OUTRAS ATIVIDADES DE ENTREGA",},
            //    new RamoAtuacao { CodigoCNAE = "I-55",Denominacao = "ALOJAMENTO",},
            //    new RamoAtuacao { CodigoCNAE = "I-56",Denominacao = "ALIMENTAÇÃO",},
            //    new RamoAtuacao { CodigoCNAE = "J-58",Denominacao = "EDIÇÃO E EDIÇÃO INTEGRADA À IMPRESSÃO",},
            //    new RamoAtuacao { CodigoCNAE = "J-59",Denominacao = "ATIVIDADES CINEMATOGRÁFICAS, PRODUÇÃO DE VÍDEOS E DE PROGRAMAS DE TELEVISÃO; GRAVAÇÃO DE SOM E EDIÇÃO DE MÚSICA",},
            //    new RamoAtuacao { CodigoCNAE = "J-60",Denominacao = "ATIVIDADES DE RÁDIO E DE TELEVISÃO",},
            //    new RamoAtuacao { CodigoCNAE = "J-61",Denominacao = "TELECOMUNICAÇÕES",},
            //    new RamoAtuacao { CodigoCNAE = "J-62",Denominacao = "ATIVIDADES DOS SERVIÇOS DE TECNOLOGIA DA INFORMAÇÃO",},
            //    new RamoAtuacao { CodigoCNAE = "J-63",Denominacao = "ATIVIDADES DE PRESTAÇÃO DE SERVIÇOS DE INFORMAÇÃO",},
            //    new RamoAtuacao { CodigoCNAE = "K-64",Denominacao = "ATIVIDADES DE SERVIÇOS FINANCEIROS",},
            //    new RamoAtuacao { CodigoCNAE = "K-65",Denominacao = "SEGUROS, RESSEGUROS, PREVIDÊNCIA COMPLEMENTAR E PLANOS DE SAÚDE",},
            //    new RamoAtuacao { CodigoCNAE = "K-66",Denominacao = "ATIVIDADES AUXILIARES DOS SERVIÇOS FINANCEIROS, SEGUROS, PREVIDÊNCIA COMPLEMENTAR E PLANOS DE SAÚDE",},
            //    new RamoAtuacao { CodigoCNAE = "L-68",Denominacao = "ATIVIDADES IMOBILIÁRIAS",},
            //    new RamoAtuacao { CodigoCNAE = "M-69",Denominacao = "ATIVIDADES JURÍDICAS, DE CONTABILIDADE E DE AUDITORIA",},
            //    new RamoAtuacao { CodigoCNAE = "M-70",Denominacao = "ATIVIDADES DE SEDES DE EMPRESAS E DE CONSULTORIA EM GESTÃO EMPRESARIAL",},
            //    new RamoAtuacao { CodigoCNAE = "M-71",Denominacao = "SERVIÇOS DE ARQUITETURA E ENGENHARIA; TESTES E ANÁLISES TÉCNICAS",},
            //    new RamoAtuacao { CodigoCNAE = "M-72",Denominacao = "PESQUISA E DESENVOLVIMENTO CIENTÍFICO",},
            //    new RamoAtuacao { CodigoCNAE = "M-73",Denominacao = "PUBLICIDADE E PESQUISA DE MERCADO",},
            //    new RamoAtuacao { CodigoCNAE = "M-74",Denominacao = "OUTRAS ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS",},
            //    new RamoAtuacao { CodigoCNAE = "M-75",Denominacao = "ATIVIDADES VETERINÁRIAS",},
            //    new RamoAtuacao { CodigoCNAE = "N-77",Denominacao = "ALUGUÉIS NÃO IMOBILIÁRIOS E GESTÃO DE ATIVOS INTANGÍVEIS NÃO FINANCEIROS",},
            //    new RamoAtuacao { CodigoCNAE = "N-78",Denominacao = "SELEÇÃO, AGENCIAMENTO E LOCAÇÃO DE MÃO DE OBRA",},
            //    new RamoAtuacao { CodigoCNAE = "N-79",Denominacao = "AGÊNCIAS DE VIAGENS, OPERADORES TURÍSTICOS E SERVIÇOS DE RESERVAS",},
            //    new RamoAtuacao { CodigoCNAE = "N-80",Denominacao = "ATIVIDADES DE VIGILÂNCIA, SEGURANÇA E INVESTIGAÇÃO",},
            //    new RamoAtuacao { CodigoCNAE = "N-81",Denominacao = "SERVIÇOS PARA EDIFÍCIOS E ATIVIDADES PAISAGÍSTICAS",},
            //    new RamoAtuacao { CodigoCNAE = "N-82",Denominacao = "SERVIÇOS DE ESCRITÓRIO, DE APOIO ADMINISTRATIVO E OUTROS SERVIÇOS PRESTADOS PRINCIPALMENTE ÀS EMPRESAS",},
            //    new RamoAtuacao { CodigoCNAE = "O-84",Denominacao = "ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL",},
            //    new RamoAtuacao { CodigoCNAE = "P-85",Denominacao = "EDUCAÇÃO",},
            //    new RamoAtuacao { CodigoCNAE = "Q-86",Denominacao = "ATIVIDADES DE ATENÇÃO À SAÚDE HUMANA",},
            //    new RamoAtuacao { CodigoCNAE = "Q-87",Denominacao = "ATIVIDADES DE ATENÇÃO À SAÚDE HUMANA INTEGRADAS COM ASSISTÊNCIA SOCIAL, PRESTADAS EM RESIDÊNCIAS COLETIVAS E PARTICULARES",},
            //    new RamoAtuacao { CodigoCNAE = "Q-88",Denominacao = "SERVIÇOS DE ASSISTÊNCIA SOCIAL SEM ALOJAMENTO",},
            //    new RamoAtuacao { CodigoCNAE = "R-90",Denominacao = "ATIVIDADES ARTÍSTICAS, CRIATIVAS E DE ESPETÁCULOS",},
            //    new RamoAtuacao { CodigoCNAE = "R-91",Denominacao = "ATIVIDADES LIGADAS AO PATRIMÔNIO CULTURAL E AMBIENTAL",},
            //    new RamoAtuacao { CodigoCNAE = "R-92",Denominacao = "ATIVIDADES DE EXPLORAÇÃO DE JOGOS DE AZAR E APOSTAS",},
            //    new RamoAtuacao { CodigoCNAE = "R-93",Denominacao = "ATIVIDADES ESPORTIVAS E DE RECREAÇÃO E LAZER",},
            //    new RamoAtuacao { CodigoCNAE = "S-94",Denominacao = "ATIVIDADES DE ORGANIZAÇÕES ASSOCIATIVAS",},
            //    new RamoAtuacao { CodigoCNAE = "S-95",Denominacao = "REPARAÇÃO E MANUTENÇÃO DE EQUIPAMENTOS DE INFORMÁTICA E COMUNICAÇÃO E DE OBJETOS PESSOAIS E DOMÉSTICOS",},
            //    new RamoAtuacao { CodigoCNAE = "S-96",Denominacao = "OUTRAS ATIVIDADES DE SERVIÇOS PESSOAIS",},
            //    new RamoAtuacao { CodigoCNAE = "T-97",Denominacao = "SERVIÇOS DOMÉSTICOS",},
            //    new RamoAtuacao { CodigoCNAE = "U-99",Denominacao = "ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS",},
            //};



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

            //context.AddRange(empJuniores);
            //context.AddRange(tags);
            context.AddRange(ramosAtuacao);
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

        private static List<RamoAtuacao> lerListaCNAE(string WebRootPath)
        {
            //Preencher tabela de ramos de atuação a partir de um CSV extraído do IBGE
            string csvPath = Path.Combine(WebRootPath, "csv/cnae.csv");
            string csvData = File.ReadAllText(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] {
                new DataColumn("CodigoCNAE", typeof(string)),
                new DataColumn("Denominacao", typeof(string)),
            });

            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;

                    //Colunas separadas por barra
                    foreach (string cell in row.Split('/'))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }

            var ramosAtuacao = new List<RamoAtuacao>();
            foreach (DataRow row in dt.Rows)
            {
                ramosAtuacao.Add(new RamoAtuacao()
                {
                    CodigoCNAE = row["CodigoCNAE"].ToString(),
                    Denominacao = row["Denominacao"].ToString()
                });
            }

            return ramosAtuacao;
        }
    }
}