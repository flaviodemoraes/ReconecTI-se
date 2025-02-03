using MediatR;
using WebApp.Models;

namespace WebApp.Queries
{
    public record BuscarPessoaQuery(int Id) : IRequest<Pessoa>;
    
}
