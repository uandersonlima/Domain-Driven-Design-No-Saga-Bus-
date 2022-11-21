using AscoreStore.Catalog.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AscoreStore.Catalog.Data.Mappings
{
    public class ImageMapping : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(c => c.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

            builder.Property(c => c.ContentType)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Size)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Data)
                .IsRequired()
                .HasColumnType("longblob");
        }
    }
}