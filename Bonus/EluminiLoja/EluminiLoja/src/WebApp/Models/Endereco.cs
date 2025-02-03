namespace WebApp.Models
{
    public class Endereco
    {
        public virtual int Id { get; set; }
        public virtual string Rua { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string Estado { get; set; }
    }

}
