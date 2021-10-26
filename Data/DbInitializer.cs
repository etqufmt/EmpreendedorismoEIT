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
                new RamoAtuacao { CNAE = "01-03 AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA",},
                new RamoAtuacao { CNAE = "05-09 INDÚSTRIAS EXTRATIVAS",},
                new RamoAtuacao { CNAE = "10-33 INDÚSTRIAS DE TRANSFORMAÇÃO",},
                new RamoAtuacao { CNAE = "35-35 ELETRICIDADE E GÁS",},
                new RamoAtuacao { CNAE = "36-39 ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO",},
                new RamoAtuacao { CNAE = "41-43 CONSTRUÇÃO",},
                new RamoAtuacao { CNAE = "45-47 COMÉRCIO E REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS",},
                new RamoAtuacao { CNAE = "49-53 TRANSPORTE, ARMAZENAGEM E CORREIO",},
                new RamoAtuacao { CNAE = "55-56 ALOJAMENTO E ALIMENTAÇÃO",},
                new RamoAtuacao { CNAE = "58-63 INFORMAÇÃO E COMUNICAÇÃO",},
                new RamoAtuacao { CNAE = "64-66 ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS",},
                new RamoAtuacao { CNAE = "68-68 ATIVIDADES IMOBILIÁRIAS",},
                new RamoAtuacao { CNAE = "69-75 ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS",},
                new RamoAtuacao { CNAE = "77-82 ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES",},
                new RamoAtuacao { CNAE = "84-84 ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL",},
                new RamoAtuacao { CNAE = "85-85 EDUCAÇÃO",},
                new RamoAtuacao { CNAE = "86-88 SAÚDE HUMANA E SERVIÇOS SOCIAIS",},
                new RamoAtuacao { CNAE = "90-93 ARTES, CULTURA, ESPORTE E RECREAÇÃO",},
                new RamoAtuacao { CNAE = "94-96 OUTRAS ATIVIDADES DE SERVIÇOS",},
                new RamoAtuacao { CNAE = "97-97 SERVIÇOS DOMÉSTICOS",},
                new RamoAtuacao { CNAE = "99-99 ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS",},
            };

            var tags = new Tag[]
            {
                new Tag
                {
                    Nome = "Tecnologia da Informação",
                    Cor = 0xFECE67,
                },
                new Tag
                {
                    Nome = "Engenharia",
                    Cor = 0x999966,
                },
                new Tag
                {
                    Nome = "Geologia",
                    Cor = 0x75C0E0,
                },
                new Tag
                {
                    Nome = "Agricultura",
                    Cor = 0xB676B1,
                },
                new Tag
                {
                    Nome = "Saneamento",
                    Cor = 0xEE778F,
                },
                new Tag
                {
                    Nome = "Consultoria",
                    Cor = 0x42BD88,
                },
                new Tag
                {
                    Nome = "Construção Civil",
                    Cor = 0xFFAA80,
                },
                new Tag
                {
                    Nome = "Automação",
                    Cor = 0x9999FF,
                },
                new Tag
                {
                    Nome = "Pecuária",
                    Cor = 0x82CAAF,
                },
                new Tag
                {
                    Nome = "Veterinária",
                    Cor = 0xEE778F,
                },
            };

            var empJuniores = new Empresa[]
            {
                new Empresa
                {
                    Nome = "Fácil Consultoria",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "33004540000100",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Englobal",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "34110512000122",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Inovação Rural",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "36593522000191",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.SINOP,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Aliquam",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "59858191000170",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Innovates Engenharia",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "89461112000153",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.ARAGUAIA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Solução Florestal",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "09672396000198",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "CIM",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "34588525000101",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Ascin Florestal Jr.",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "87293260000180",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.SINOP,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Infocorp",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "05402978000101",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Agrovale",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "04547840000120",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.ARAGUAIA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Seres",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "05380786000133",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "EJEA",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "81149374000175",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.ARAGUAIA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Inova",
                    Tipo = Tipo.JUNIOR,
                    CNPJ = "06549035000160",
                    Segmento = Segmento.SERVICOS,
                    RamoAtuacao = ramosAtuacao[0],
                    Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais.",
                    Endereco = "Av. Fernando Corrêa da Costa, SN – SALA 101 – FE – Campus da UFMT 78060-900 – Cuiabá – MT",
                    Telefone = "6532323232",
                    Email = "ej.facilconsultoria@gmail.com",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosJunior = new DadosJunior
                    {
                        Campus = Campus.CUIABA,
                        Instituto = "FACC"
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.INSTAGRAM,
                            URL = "https://www.instagram.com/ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.LINKEDIN,
                            URL = "https://www.linkedin.com/company/senaimt"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[4],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[5],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[6],
                            Grau = 0.5,
                        },
                    }
                },
                new Empresa
                {
                    Nome = "Loggi",
                    Tipo = Tipo.INCUBADA,
                    CNPJ = "65645566000116",
                    Segmento = Segmento.COMERCIO,
                    RamoAtuacao = ramosAtuacao[16],
                    Descricao = "Loggi é um serviço de logística e entrega. Utiliza tecnologia para melhorar a logística em todo o Brasil, fornecendo uma rede de entrega sob demanda, rápida, econômica e confiável para e-commerce, entrega de alimentos por atacado e serviços de courier, juntamente com preços transparentes, pagamento automático e rastreamento em tempo real.",
                    Endereco = "R. Girassol, 555 - Vila Madalena, São Paulo - SP, 05433-001",
                    Telefone = "65545454545",
                    Email = "email@loggi.com.br",
                    Situacao = Situacao.ATIVA,
                    UltimaModificacao = DateTime.Now,
                    DadosIncubada = new DadosIncubada
                    {
                        Edital = "Edital XXV de 2020",
                        MesEntrada = new DateTime(2020,06,01),
                        MesSaida = new DateTime(2022,06,01),
                    },
                    ProdServicos = new List<ProdServico>
                    {
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                        new ProdServico
                        {
                            Nome = "Estudo de viabilidade econômica",
                            Descricao = "Consiste em fornecer informações ao gestor a respeito da viabilidade ou inviabilidade do seu investimento, a fim de auxiliá-lo na tomada de decisão de um novo projeto e/ou na ampliação/redução de um projeto já existente. Por meio de um estudo mercadológico e financeiro, utilizando projeções e indicadores, é possível analisar as condições de tal investimento."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de marketing",
                            Descricao = "É feita uma análise do perfil do consumidor, do público-alvo, de ações de divulgação e comunicação, além de preço, distribuição e localização do ponto de venda. São desenvolvidas ações para satisfazer os clientes, posicionar a marca e promover o sucesso do negócio."
                        },
                        new ProdServico
                        {
                            Nome = "Plano de negócios",
                            Descricao = "Planeja e organiza a empresa em todos os âmbitos. Estrutura novos negócios, permitindo segurança ao entrar no mercado, e reestrutura as já existentes, solidificando a organização. Nosso serviço mais completo é um guia sobre todas as ações empresariais."
                        },
                    },
                    RedesSociais = new List<RedeSocial>
                    {
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WEBSITE,
                            URL = "https://ufmt.br"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.FACEBOOK,
                            URL = "https://pt-br.facebook.com/ufmatogrosso"
                        },
                        new RedeSocial
                        {
                            Plataforma = Plataforma.WHATSAPP,
                            URL = "65999998888"
                        },
                    },
                    TagsAssociadas = new List<EmpresaTag>
                    {
                        new EmpresaTag
                        {
                            Tag = tags[0],
                            Grau = 0.5,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[1],
                            Grau = 0.4,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[2],
                            Grau = 0.3,
                        },
                        new EmpresaTag
                        {
                            Tag = tags[3],
                            Grau = 0.2,
                        },
                    }
                },
            };


            context.AddRange(ramosAtuacao);
            context.AddRange(tags);
            context.AddRange(empJuniores);
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