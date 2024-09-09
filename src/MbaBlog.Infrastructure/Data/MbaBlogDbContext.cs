using MbaBlog.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace MbaBlog.Mvc.Data
{
    public class MbaBlogDbContext : DbContext
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ComentarioPost> Comentarios { get; set; }
        
        public MbaBlogDbContext(DbContextOptions<MbaBlogDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new AutorConfiguration());
            //builder.ApplyConfiguration(new PostConfiguration());
            //builder.ApplyConfiguration(new ComentarioPostConfiguration());

            builder.ApplyConfigurationsFromAssembly(typeof(MbaBlogDbContext).Assembly);
        }

    }
}
