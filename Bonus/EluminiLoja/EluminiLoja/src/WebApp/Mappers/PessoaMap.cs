using FluentNHibernate.Mapping;
using WebApp.Models;

namespace WebApp.Mappers
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Table("Pessoas");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nome).Not.Nullable();
            References(x => x.Endereco).Cascade.All();
        }
    }
}
