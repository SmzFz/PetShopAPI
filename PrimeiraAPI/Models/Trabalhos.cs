using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Trabalhos
    {

        public Guid TrabalhosId { get; set; }

        [Required(ErrorMessage = "O campo Data Do Trabalho é obrigatório")]
        [Display(Name = "Data de Trabalho")]
        public float ValorTrabalho { get; set; }


        [Required(ErrorMessage = "O campo Data De trabalho é obrigatório")]
        [Display(Name = "Data de trabalho")]
        public DateTime DataTrabalho { get; set; }

        public int VenderId { get; set; }
        public Vender? Vender { get; set; }
    }
}
