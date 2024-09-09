using MbaBlog.Domain.Domain.Commun;

namespace MbaBlog.Domain.Domain
{
    public class Post : EntityBase
    {
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }
        public required string Titulo { get; set; }
        public required string Texto { get; set; }
        public ICollection<ComentarioPost>? Comentarios { get; set; }
    }
}
