using Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Product
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
               .ToTable(nameof(Category));
            builder.Property(nameof(Category.Name)).HasMaxLength(200).IsRequired();
            builder.Property(nameof(Category.Description)).HasMaxLength(500);
        }
    }
}
