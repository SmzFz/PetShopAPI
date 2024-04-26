using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Vender
    {
        public Guid VenderId { get; set; }


        [Required(ErrorMessage = "O campo data de venda é obrigatório")]

        [Display(Name = "Data da Venda")]
        public DateTime VendaData { get; set; }


        [Required(ErrorMessage = "O campo Valor da venda é obrigatório")]
        [Display(Name = "Valor da Venda")]
        public float? VendaValor { get; set; }


        [Required(ErrorMessage = "O campo Avaliação da venda é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo deve ter entre 3 e 100 caracteres")]
        [Display(Name = "Avaliação da Venda")]
        public string? VendaAvaliacao { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
