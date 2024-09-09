using MbaBlog.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbaBlog.Mvc.Data.Configurations
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autores");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("VARCHAR(25)").IsRequired();

            builder.HasIndex(i => i.Email).HasName("idx_cliente_email");

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.Autor)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
