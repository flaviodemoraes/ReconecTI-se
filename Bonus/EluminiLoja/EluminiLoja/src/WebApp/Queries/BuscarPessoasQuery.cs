using MediatR;
using WebApp.Models;

namespace WebApp.Queries
{
    public record BuscarPessoasQuery() : IRequest<IEnumerable<Pessoa>>;
}
