using MediatR;
using WebApp.Models;
using WebApp.Queries;

namespace WebApp.Handlers
{
    public class BuscarPessoaHandler : IRequestHandler<BuscarPessoaQuery, Pessoa>
    {
        private readonly NHibernate.ISession _session;

        public BuscarPessoaHandler(NHibernate.ISession session)
        {
            _session = session;
        }

        public async Task<Pessoa> Handle(BuscarPessoaQuery request, CancellationToken cancellationToken)
        {
            return await _session.GetAsync<Pessoa>(request.Id, cancellationToken);
        }
    }
}
