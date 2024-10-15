using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MbaBlog.Data.Domain.Commun;

namespace MbaBlog.Data.Domain;

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
    [StringLength(80, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
    public required string Titulo { get; set; }

    [DisplayName("Texto")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(1500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 100)]
    public required string Texto { get; set; }

    public IEnumerable<ComentarioPost>? Comentarios { get; set; }
}
