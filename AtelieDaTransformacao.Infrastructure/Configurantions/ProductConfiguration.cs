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

            builder.Property(p => p.Title) //Inicia configuração da propriedade Name; retorna um configurador de propriedade.

                .IsRequired() //Torna a coluna Name obrigatória (não nula) no banco.
                .HasMaxLength(100);//Define comprimento máximo de 100 caracteres para a coluna Name.

            builder.Property(p => p.Description) // Inicia configuração da propriedade Description.

                .HasMaxLength(500);//Define comprimento máximo de 500 caracteres para Description; não marca como obrigatória (pode ser nula).

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); //•	Configura tipo do preço.

            builder.HasOne(p => p.Category) //•	Configura relacionamento 1 (Product) -> N (ProductCategory): indica que Product tem a navegação chamada 'Category'.
                .WithMany(c => c.Product) // •	Especifica o outro lado do relacionamento: ProductCategory expõe a coleção 'Product' (atenção: nome no entity é singular).
                .HasForeignKey(p => p.CategoryId); // •	Define CategoryId como chave estrangeira no Product apontando para ProductCategory.
        }
    }
}