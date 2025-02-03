using FluentNHibernate.Mapping;
using WebApp.Models;

namespace WebApp.Mappers
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Table("Enderecos");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Rua).Not.Nullable();
            Map(x => x.Cidade).Not.Nullable();
            Map(x => x.Estado).Not.Nullable();
        }
    }
}
