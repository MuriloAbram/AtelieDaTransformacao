using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product> //Declara a classe pública que implementa IEntityTypeConfiguration<Product>, indicando que fornece a configuração para a entidade Product.
    {
        public void Configure(EntityTypeBuilder<Product> builder) // Implementa o método Configure exigido pela interface; recebe um builder para definir o mapeamento da entidade.
        {
            builder.HasKey(p => p.Id); //Define Id como chave primária da entidade Product.

            builder.Property(p => p.Name) //Inicia configuração da propriedade Name; retorna um configurador de propriedade.

                .IsRequired() //Torna a coluna Name obrigatória (não nula) no banco.
                .HasMaxLength(100);//Define comprimento máximo de 100 caracteres para a coluna Name.

            builder.Property(p => p.Description) // Inicia configuração da propriedade Description.

                .HasMaxLength(500);//Define comprimento máximo de 500 caracteres para Description; não marca como obrigatória (pode ser nula).

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); //•	Configura relacionamento 1 (Product) -> 1/Many (ProductCategory): indica que Product tem uma referência a ProductCategory.

            builder.HasOne(p => p.ProductCategory) //•	Configura relacionamento 1 (Product) -> 1/Many (ProductCategory): indica que Product tem uma referência a ProductCategory.
                .WithMany(c => c.Products) // •	Especifica o outro lado do relacionamento: ProductCategory tem muitos Products.
                .HasForeignKey(p => p.ProductCategoryId);// •	Define ProductCategoryId como chave estrangeira no Product apontando para ProductCategory.
        }
    }
}