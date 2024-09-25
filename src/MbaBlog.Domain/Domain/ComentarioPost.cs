using MbaBlog.Domain.Domain.Commun;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MbaBlog.Domain.Domain
{
    public class ComentarioPost : EntityBase
    {
        [ScaffoldColumn(false)]
        public Guid PostId { get; set; }

        [ScaffoldColumn(false)]
        public Guid AutorId { get; set; }

        [ScaffoldColumn(false)]
        public Post? Post { get; set; }

        [DisplayName("Comentario")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public required string Comentario { get; set; }

    }
}
