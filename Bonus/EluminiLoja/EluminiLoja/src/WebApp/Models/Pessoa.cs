namespace WebApp.Models
{
    public class Pessoa
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual Endereco Endereco { get; set; }
    }

}
