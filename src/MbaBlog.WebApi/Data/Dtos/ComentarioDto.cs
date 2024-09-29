using MbaBlog.Domain.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MbaBlog.WebApi.Data.Dtos
{
    public class ComentarioDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid PostId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid AutorId { get; set; }

        [DisplayName("Comentario")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public required string Comentario { get; set; }
    }
}
