using MbaBlog.Domain.Domain.Commun;

namespace MbaBlog.Domain.Domain
{
    public class Autor : EntityBase
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
