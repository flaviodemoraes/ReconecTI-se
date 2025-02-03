using EfDbExistente.DTO;
using EfDbExistente.Infrastructure.Data;
using EfDbExistente.Repositories;

namespace EfDbExistente.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<ProdutoDTO>> ListarProdutosDTO() 
        {
            var produtos = await _produtoRepository.GetAll();

            return produtos.Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria != null ? p.Categoria.Nome : "Sem Categoria",
                Quantidade = p.Quantidade,
                Preco = p.Preco,
                Ativo = p.Ativo,
                DataCadastro = p.DataCadastro,
                DataDesativacao = p.DataDesativacao,
                DataEntrada = p.DataEntrada,
                DataSaida = p.DataSaida
            });
        }
            

        public async Task<ProdutoDTO> ObterProdutoDTO(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                CategoriaId = produto.CategoriaId,
                CategoriaNome = produto.Categoria != null ? produto.Categoria.Nome : "Sem Categoria",
                Quantidade = produto.Quantidade,
                Preco = produto.Preco,
                Ativo = produto.Ativo,
                DataCadastro = produto.DataCadastro,
                DataDesativacao = produto.DataDesativacao,
                DataEntrada = produto.DataEntrada,
                DataSaida = produto.DataSaida
            };
        }

        public async Task<Produto> ObterProduto(int id) => 
            await _produtoRepository.GetById(id);

        public async Task AdicionarProduto(Produto produto) =>
            await _produtoRepository.Add(produto);

        public async Task AtualizarProduto(Produto produto) =>
            await _produtoRepository.Update(produto);

        public async Task DeletarProduto(int id) =>
            await _produtoRepository.Delete(id);

        public async Task DesativarProduto(int id) =>
            await _produtoRepository.DesativarProduto(await _produtoRepository.GetById(id));

        public void Dispose() => _produtoRepository.Dispose();
    }
}
