using MbaBlog.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbaBlog.Data.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.AutorId).IsRequired();
        builder.Property(p => p.CriadoEm);
        builder.Property(p => p.ModificadoEm);
        builder.Property(p => p.Titulo).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(p => p.Texto).HasColumnType("VARCHAR(1500)").IsRequired();

        builder.HasMany(p => p.Comentarios)
            .WithOne(p => p.Post)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
