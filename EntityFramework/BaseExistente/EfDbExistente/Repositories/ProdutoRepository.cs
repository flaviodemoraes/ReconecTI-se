using EfDbExistente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EfDbExistente.Repositories
{
    public class ProdutoRepository : IRepository<Produto>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Produto>> GetAll() =>
            await _context.Produtos.Include(p => p.Categoria).ToListAsync();

        public async Task<Produto> GetById(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                Console.WriteLine($"Produto com ID {id} não encontrado!");

            return produto;
        }

        public async Task Add(Produto produto)
        {
            try
            {
                await _context.Produtos.AddAsync(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                throw;
            }

        }

        public async Task Update(Produto produto)
        {
            try
            {
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                throw;
            }

        }

        public async Task Delete(int id)
        {
            var produto = await GetById(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DesativarProduto(Produto produto)
        {
            produto.DesativarProduto();
            await Update(produto);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
