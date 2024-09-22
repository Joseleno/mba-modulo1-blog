using MbaBlog.Domain.Domain.Commun;
using System.Reflection.Metadata.Ecma335;

namespace MbaBlog.Domain.Domain
{
    public class ComentarioPost : EntityBase
    {
        public Guid PostId { get; set; }
        public Guid AutorId { get; set; }
        public Post? Post { get; set; }
        public required string Comentario { get; set; }
    }
}
