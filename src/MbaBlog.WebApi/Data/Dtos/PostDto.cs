using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MbaBlog.WebApi.Data.Dtos
{
    public class PostDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required Guid AutorId { get; set; }

        [DisplayName("Titulo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(80, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]

        public required string Titulo { get; set; }

        [DisplayName("Texto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 100)]
        public required string Texto { get; set; }
    }
}
