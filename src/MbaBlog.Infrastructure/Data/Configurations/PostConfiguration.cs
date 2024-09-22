using MbaBlog.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbaBlog.Infrastructure.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.AutorId).IsRequired();
        builder.Property(p => p.CriadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(p => p.ModificadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnUpdate();
        builder.Property(p => p.Titulo).HasColumnType("VARCHAR(30)").IsRequired();
        builder.Property(p => p.Texto).HasColumnType("VARCHAR(1024)").IsRequired();

        builder.HasMany(p => p.Comentarios)
            .WithOne(p => p.Post)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
