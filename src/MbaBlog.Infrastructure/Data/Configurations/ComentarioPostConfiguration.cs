using MbaBlog.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbaBlog.Infrastructure.Data.Configurations;

public class ComentarioPostConfiguration : IEntityTypeConfiguration<ComentarioPost>
{
    public void Configure(EntityTypeBuilder<ComentarioPost> builder)
    {
        builder.ToTable("Comentarios");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.PostId).IsRequired();
        builder.Property(p => p.AutorId).IsRequired();
        builder.Property(p => p.Comentario).HasColumnType("VARCHAR(500)").IsRequired();
    }
}
