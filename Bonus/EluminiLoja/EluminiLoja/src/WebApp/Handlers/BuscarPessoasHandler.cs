using MediatR;
using WebApp.Models;
using WebApp.Queries;

namespace WebApp.Handlers
{
    public class BuscarPessoasHandler : IRequestHandler<BuscarPessoasQuery, IEnumerable<Pessoa>>
    {
        private readonly NHibernate.ISession _session;

        public BuscarPessoasHandler(NHibernate.ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Pessoa>> Handle(BuscarPessoasQuery request, CancellationToken cancellationToken)
        {
            var criteria = await _session.CreateCriteria<Pessoa>().ListAsync<Pessoa>(cancellationToken);
            return criteria.ToList();
        }
    }
}
