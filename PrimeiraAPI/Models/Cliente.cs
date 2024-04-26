using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do cliente deve ter entre 3 e 100 caracteres")]
        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "O e-mail do cliente é obrigatório")]
        [Display(Name = "Email do Cliente")]
        [EmailAddress(ErrorMessage = "O e-mail do cliente deve estar em um formato válido (exemplo: nome@provedor.com)")]
        public string EmailCliente { get; set; }


        [Required(ErrorMessage = "O CPF do cliente é obrigatório")]
        [Display(Name = "CPF do Cliente")]
        [StringLength(11, MinimumLength = 1, ErrorMessage = "O CPF do cliente deve ter 11 caracteres")]
        public string CpfCliente { get; set; }

        [Required(ErrorMessage = "O CEP do cliente é obrigatório")]
        [Display(Name = "CEP do Cliente")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "O CEP do cliente deve ter 8 caracteres")]
        public string CepCliente { get; set; }

        [Required(ErrorMessage = "O Telefone do cliente é obrigatório")]
        [Display(Name = "Telefone do Cliente")]
        [StringLength(9, MinimumLength = 1, ErrorMessage = "O Telefone do cliente deve ter 9 caracteres")]
        public string TelCliente { get; set; }

    }
}
