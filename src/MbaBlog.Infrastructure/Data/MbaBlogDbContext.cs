using MbaBlog.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace MbaBlog.Infrastructure.Data;

public class MbaBlogDbContext(DbContextOptions<MbaBlogDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<ComentarioPost> Comentarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.ApplyConfiguration(new AutorConfiguration());
        //builder.ApplyConfiguration(new PostConfiguration());
        //builder.ApplyConfiguration(new ComentarioPostConfiguration());

        builder.ApplyConfigurationsFromAssembly(typeof(MbaBlogDbContext).Assembly);
    }

}
