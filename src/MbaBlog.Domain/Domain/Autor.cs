using MbaBlog.Domain.Domain.Commun;

namespace MbaBlog.Domain.Domain
{
    public class Autor : EntityBase
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
