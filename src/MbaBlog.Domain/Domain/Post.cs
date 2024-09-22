using MbaBlog.Domain.Domain.Commun;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MbaBlog.Domain.Domain
{
    public class Post : EntityBase
    {
        public Guid AutorId { get; set; }

        public DateTime CriadoEm { get; set; }

        public DateTime? ModificadoEm { get; set; }

        public required string Titulo { get; set; }

        public required string Texto { get; set; }

        public IEnumerable<ComentarioPost>? Comentarios { get; set; }
    }
}
