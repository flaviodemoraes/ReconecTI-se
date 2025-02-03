using MediatR;
using WebApp.Commands;
using WebApp.Models;

namespace WebApp.Handlers
{
    public class CriarPessoaHandler : IRequestHandler<CriarPessoaCommand, int>
    {
        private readonly NHibernate.ISession _session;

        public CriarPessoaHandler(NHibernate.ISession session)
        {
            _session = session;
        }

        public async Task<int> Handle(CriarPessoaCommand request, CancellationToken cancellationToken)
        {
            var endereco = new Endereco
            {
                Rua = request.Rua,
                Cidade = request.Cidade,
                Estado = request.Estado
            };

            var pessoa = new Pessoa
            {
                Nome = request.Nome,
                Endereco = endereco
            };

            await _session.SaveAsync(endereco, cancellationToken);
            await _session.SaveAsync(pessoa, cancellationToken);
            await _session.FlushAsync(cancellationToken);

            return pessoa.Id;
        }
    }
}
