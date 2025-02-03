namespace EfDbExistente.DTO
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataDesativacao { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
    }
}
