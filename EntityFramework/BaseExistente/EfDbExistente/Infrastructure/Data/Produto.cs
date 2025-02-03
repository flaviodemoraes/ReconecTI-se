using System.Text.Json.Serialization;

namespace EfDbExistente.Infrastructure.Data;

public partial class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int CategoriaId { get; set; }

    public decimal Quantidade { get; set; }

    public decimal Preco { get; set; }

    public bool? Ativo { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataDesativacao { get; set; }

    public DateTime? DataEntrada { get; set; }

    public DateTime? DataSaida { get; set; }

    [JsonIgnore]
    public virtual Categoria Categoria { get; set; } = null!;

    public Produto(string nome, int categoriaId, decimal quantidade, decimal preco)
    {
        Nome = nome;
        CategoriaId = categoriaId;
        Quantidade = quantidade;
        Preco = preco;
        Ativo = true;
        DataCadastro = DateTime.Now;
        DataEntrada = DateTime.Now;
    }

    public void DesativarProduto()
    {
        Ativo = false;
        DataDesativacao = DateTime.Now;
    }

    public void BaixarQuantidade(decimal quantidadeBaixa)
    {
        Quantidade -= quantidadeBaixa;
        DataSaida = DateTime.Now;
    }

    public void EntradaQuantidade(decimal quantidadeEntrada)
    {
        Quantidade += quantidadeEntrada;
        DataEntrada = DateTime.Now;
    }
}
