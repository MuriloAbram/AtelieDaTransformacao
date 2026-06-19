using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Infrastructure.Configurations
{
    public class ProductCategoryConfiguration : //Declaração da classe pública que conterá as regras de mapeamento para a entidade ProductCategory.
        IEntityTypeConfiguration<ProductCategory> //A classe implementa a interface IEntityTypeConfiguration<T> do EF Core, sinalizando que fornece a configuração para ProductCategory.
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder) // Implementação do método Configure exigido pela interface; recebe um EntityTypeBuilder<ProductCategory> que permite definir mapeamentos e restrições.
        {
            builder.HasKey(c => c.Id); //Define a propriedade Id como chave primária da entidade ProductCategory no modelo do EF Core.

            builder.Property(c => c.Name) //Inicia a configuração da propriedade Name da entidade; retorna um objeto configurador de propriedade.
                .IsRequired()
                .HasMaxLength(80);
        }
    }
}