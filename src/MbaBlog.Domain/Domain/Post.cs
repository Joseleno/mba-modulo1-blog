using MbaBlog.Domain.Domain.Commun;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MbaBlog.Domain.Domain
{
    public class Post : EntityBase
    {
        [ScaffoldColumn(false)]
        public Guid AutorId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CriadoEm { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ModificadoEm { get; set; }

        [DisplayName("Titulo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public required string Titulo { get; set; }

        [DisplayName("Texto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 20)]
        public required string Texto { get; set; }

        public IEnumerable<ComentarioPost>? Comentarios { get; set; }
    }
}
