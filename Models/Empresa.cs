using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("Empresas")]
    public class Empresa
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public Tipo Tipo { get; set; }

        [StringLength(11)]
        public string CNPJ { get; set; }

        public Segmento Segmento { get; set; }

        public int RamoAtuacaoID { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        [StringLength(200)]
        public string Endereco { get; set; }

        [StringLength(11)]
        public string Telefone { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Logo { get; set; }

        [Required]
        public Situacao Situacao { get; set; }

        [DataType(DataType.Date)]
        public DateTime UltimaModificacao { get; set; }

        //Outras entidades
        public RamoAtuacao RamoAtuacao { get; set; }
        public DadosIncubada DadosIncubada { get; set; }
        public DadosJunior DadosJunior { get; set; }
        public ICollection<RedeSocial> RedesSociais { get; set; }
        public ICollection<ProdutoServico> ProdutosServicos { get; set; }
        public ICollection<EmpresaTag> TagsAssociadas { get; set; }
    }

    public enum Tipo
    {
        [Display(Name = "Empresa Júnior")]
        JUNIOR = 1,

        [Display(Name = "Empresa Incubada")]
        INCUBADA = 2
    }

    public enum Situacao
    {
        [Display(Name = "Ativa")]
        ATIVA = 1,

        [Display(Name = "Inativa")]
        INATIVA = 2,
    }

    public enum Segmento
    {
        [Display(Name = "Indústria")]
        INDUSTRIA = 1,

        [Display(Name = "Comércio")]
        COMERCIO = 2,

        [Display(Name = "Prestação de serviços")]
        SERVICOS = 3,
    }
}
