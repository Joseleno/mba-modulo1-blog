using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MbaBlog.Domain.Domain;

namespace MbaBlog.Mvc.ViewModels;

public class ComentarioViewModel
{
    [Key]
    public Guid Id { get; set; }

    //public Guid PostId { get; set; }
    //public required string AutorId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
    public required string Comentario { get; set; }
}
