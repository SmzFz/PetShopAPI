using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Pagar
    {
        public Guid PagarId { get; set; }

        [Required(ErrorMessage = "O campo Formato De Pagamento é obrigatório")]
        [Display(Name = "Formato de Pagamento")]
        public string FormatoPagar { get; set; }

        [Required(ErrorMessage = "O campo Data De Pagamento é obrigatório")]
        [Display(Name = "Data de Pagamento")]
        public DateTime DataPagar { get; set; }

        [Required(ErrorMessage = "O campo Valor do pagamento é obrigatório")]

        [Display(Name = "Valor a Pagar")]
        public float? ValorPagar { get; set; }


        [Display(Name = "Troco")]
        public float? TrocoPagar { get; set; }



        public Guid VenderId { get; set; }
        public Vender? Vender { get; set; }
    }
}
