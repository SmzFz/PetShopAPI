using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Models
{
    public class Pets
    {
        public Guid PetsId { get; set; }


        [Required(ErrorMessage = "O nome do pet obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do pet deve ter entre 3 e 100 caracteres")]
        [Display(Name = "Nome do Pet")]
        public string NomePet { get; set; }

        [Required(ErrorMessage = "O e-mail do cliente é obrigatório")]
        [Display(Name = "Raça do Pet")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Raça do Pet deve ter entre 3 e 100 caracteres")]
        public string RacaPet { get; set; }


        [Required(ErrorMessage = "O campo idade do pet é obrigatório")]
        [Display(Name = "Idade do Pet")]
        [StringLength(2, MinimumLength = 0, ErrorMessage = "O Idade do Pet deve ter 2 caracteres")]
        public string IdadePet { get; }


        [Display(Name = "Detalhes do Pet")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Detalhes deve ter no máximo 100 caracteres")]
        public string DetalhesPet { get; }






    }
}
