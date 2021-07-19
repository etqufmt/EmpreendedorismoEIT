using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    public enum Tipo
    {
        JUNIOR, INCUBADA
    }

    public enum Situacao
    {
        ATIVA, INATIVA, SUSPENSA
    }

    public class Empresa
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        public Tipo Tipo { get; set; }

        [StringLength(140)]
        public string DescricaoCurta { get; set; }

        [StringLength(400)]
        public string DescricaoLonga { get; set; }

        [StringLength(200)]
        public string Endereco { get; set; }

        [StringLength(11, MinimumLength = 11)]
        [RegularExpression("([0-9]+)")]
        public string Telefone { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Logo { get; set; }

        [Required]
        public Situacao Situacao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UltimaModificacao { get; set; }

        //Outras entidades
        public DadosIncubada DadosIncubada { get; set; }
        public DadosJunior DadosJunior { get; set; }
        public ICollection<RedeSocial> RedesSociais { get; set; }
        public ICollection<ProdutoServico> ProdutosServicos { get; set; }
        public ICollection<EmpresaTag> TagsAssociadas { get; set; }
    }
}
