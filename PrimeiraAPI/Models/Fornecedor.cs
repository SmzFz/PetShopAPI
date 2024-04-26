using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Fornecedor
    {

        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O nome do Fornecedor é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do Fornecedor deve ter entre 3 e 100 caracteres")]
        [Display(Name = "Nome do Fornecedor")]
        public string NomeFornecedor { get; set; }

        [Required(ErrorMessage = "O e-mail do Fornecedor é obrigatório")]
        [Display(Name = "Email do Fornecedor")]
        [EmailAddress(ErrorMessage = "O e-mail do Fornecedor deve estar em um formato válido (exemplo: nome@provedor.com)")]
        public string EmailCliente { get; set; }



        [Display(Name = "Descrição do Fornecedor")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Descrição do fornecedor deve ter no maximo 100 caracteres")]
        public string CpfCliente { get; }

        public string ProdutoId { get; set; }

        public Produto? Produto { get; set; }



    }
}
