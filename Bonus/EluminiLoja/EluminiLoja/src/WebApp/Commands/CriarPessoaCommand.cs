using MediatR;

namespace WebApp.Commands
{
    public record CriarPessoaCommand(string Nome, string Rua, string Cidade, string Estado) 
        : IRequest<int>;

}
