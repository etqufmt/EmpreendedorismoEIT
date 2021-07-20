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

        [StringLength(140)]
        public string DescricaoCurta { get; set; }

        [StringLength(400)]
        public string DescricaoLonga { get; set; }

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
        public DadosIncubada DadosIncubada { get; set; }
        public DadosJunior DadosJunior { get; set; }
        public ICollection<RedeSocial> RedesSociais { get; set; }
        public ICollection<ProdutoServico> ProdutosServicos { get; set; }
        public ICollection<EmpresaTag> TagsAssociadas { get; set; }
    }

    public enum Tipo
    {
        [Display(Name = "Empresa Júnior")]
        JUNIOR,

        [Display(Name = "Empresa Incubada")]
        INCUBADA
    }

    public enum Situacao
    {
        [Display(Name = "Ativa")]
        ATIVA,

        [Display(Name = "Inativa")]
        INATIVA,

        [Display(Name = "Suspensa")]
        SUSPENSA
    }
}
