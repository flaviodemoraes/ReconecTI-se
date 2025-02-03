using System.Text.Json.Serialization;

namespace EfDbExistente.Infrastructure.Data;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
