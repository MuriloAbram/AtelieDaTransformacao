using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository //•	Executa a contagem de registros no banco de forma assíncrona e retorna o resultado.
    {
        private readonly AtelieDaTransformacaoDbContext _context; //•	Campo readonly que armazena a instância do DbContext usada para acessar o banco.

        public ProductRepository(AtelieDaTransformacaoDbContext context) //•	Construtor que recebe o contexto via injeção de dependência.
        {
            _context = context; //•	Atribui o contexto ao campo local para uso nos métodos do repositório.
        }

        public async Task<IEnumerable<Product>> GetAllAsync()//•	Método assíncrono que retorna todos os produtos do banco.
        {
            return await _context.Products//•	Inicia a consulta sobre o DbSet Products.
                .Include(p => p.Category)//•	Faz eager-loading da propriedade de navegação Category para carregar dados da categoria junto com o produto (evita lazy-loading). Observação: confirme o nome real da propriedade de navegação em Product (pode ser ProductCategory).
                .ToListAsync();//•	Executa a consulta de forma assíncrona e materializa a lista de produtos.
        }

        public async Task<Product?> GetByIdAsync(int id)//•	Método assíncrono que retorna um produto por id ou null se não encontrado.
        {
            return await _context.Products//•	Inicia a query no DbSet Products.
                .Include(p => p.Category)//•	Inclui a navegação Category na consulta.
                .FirstOrDefaultAsync(p => p.Id == id);//•	Retorna o primeiro produto que corresponde ao id, ou null se não existir.
        }

        public async Task<IEnumerable<Product>> GetFeaturedAsync()//•	Método que retorna os produtos marcados como em destaque (IsFeatured).
        {
            return await _context.Products//•	Inicia a consulta nos produtos.
                .Include(p => p.Category)//•	Inclui a categoria associada.
                .Where(p => p.IsFeatured)//•	Filtra apenas os produtos com IsFeatured == true.
                .ToListAsync();//•	Materializa a lista filtrada de forma assíncrona.
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)//•	Método que retorna produtos pertencentes a uma categoria específica pelo CategoryId.
        {
            return await _context.Products//•	Inicia a consulta.
                .Include(p => p.Category)//•	Inclui a categoria na consulta.
                .Where(p => p.CategoryId == categoryId)//•	Filtra produtos cujo CategoryId corresponde ao CategoryId fornecido. Observação: confirme se a propriedade de FK no Product é CategoryId ou ProductCategoryId.
                .ToListAsync();//•	Materializa e retorna a lista de forma assíncrona.
        }

        public async Task AddAsync(Product product)//•	Método para adicionar um novo produto ao banco de dados.
        {
            await _context.Products.AddAsync(product);//•	Adiciona a entidade ao DbSet de forma assíncrona (marca para inserção).
            await _context.SaveChangesAsync();//•	Salva a inserção no banco de dados de forma assíncrona.
        }

        public async Task UpdateAsync(Product product)//•	Método para atualizar um produto existente.
        {
            _context.Products.Update(product);//•	Marca a entidade como modificada no contexto.
            await _context.SaveChangesAsync();//•	Salva a inserção no banco de dados de forma assíncrona.
        }

        public async Task DeleteAsync(int id)//•	Método para remover um produto pelo id.
        {
            var product = await _context.Products.FindAsync(id);//•	Procura a entidade pelo id usando FindAsync (busca por chave primária e usa cache do contexto se disponível).

            if (product != null)//•	Verifica se o produto foi encontrado antes de tentar remover.
            {
                _context.Products.Remove(product);//•	Marca a entidade para remoção.
                await _context.SaveChangesAsync();//•	Salva a inserção no banco de dados de forma assíncrona.
            }
        }

        public async Task<int> CountAsync()//•	Método que retorna a quantidade total de produtos armazenados.
        {
            return await _context.Products.CountAsync();//•	Executa a contagem no banco de forma assíncrona e retorna o resultado.
        }
    }
}