using MbaBlog.Domain.Domain.Commun;

namespace MbaBlog.Domain.Domain
{
    public class ComentarioPost : EntityBase
    {
        public int PostId { get; set; }
        public Post? Post { get; set; }
        public required string Comentario { get; set; }
    }
}
