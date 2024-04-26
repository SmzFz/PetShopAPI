using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Produto
    {
        public Guid ProdutoId { get; set; }

        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Insira o nome do produto")]
        [Display(Name = "Nome do Produto")]
        public string NomeProduto { get; set; }



        [Required(ErrorMessage = "Insira do Valor do Produto")]

        [Display(Name = "Valor do Produto")]
        public float ValorProduto { get; set; }


        [Required(ErrorMessage = "O campo descrição do produto é obrigatório")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O campo descrição deve ter entre 1 e 100 caracteres")]
        [Display(Name = "Descrição do produto")]
        public string DescricaoProduto { get; set; }





        [Display(Name = "Produtos em Estoque")]
        public Double
    EstoqueProduto
        { get; set; }

    }
}
