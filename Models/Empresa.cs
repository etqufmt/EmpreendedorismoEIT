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
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        public Tipo Tipo { get; set; }

        [Required]
        [MaxLength(11)]
        public string CNPJ { get; set; }

        [Required]
        public Segmento Segmento { get; set; }

        public int? RamoAtuacaoID { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [MaxLength(200)]
        public string Endereco { get; set; }

        [MaxLength(11)]
        public string Telefone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Logo { get; set; }

        [Required]
        public Situacao Situacao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UltimaModificacao { get; set; }

        //Referências para outras entidades
        public RamoAtuacao RamoAtuacao { get; set; }
        public DadosIncubada DadosIncubada { get; set; }
        public DadosJunior DadosJunior { get; set; }
        public ICollection<RedeSocial> RedesSociais { get; set; }
        public ICollection<ProdServico> ProdServicos { get; set; }
        public ICollection<EmpresaTag> TagsAssociadas { get; set; }
    }

    public enum Tipo
    {
        [Display(Name = "Empresa júnior")]
        JUNIOR = 1,

        [Display(Name = "Empresa incubada")]
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
