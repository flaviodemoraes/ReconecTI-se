using MediatR;
using NHibernate;
using WebApp.Commands;
using WebApp.Handlers;
using WebApp.Queries;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configuração de serviços
// Registra o SessionFactory como Singleton
builder.Services.AddSingleton(provider => NHibernateHelper.CreateSessionFactory(connectionString));

// Registra a Session como Scoped
builder.Services.AddScoped(provider =>
{
    var sessionFactory = provider.GetService<ISessionFactory>();
    if (sessionFactory == null)
    {
        throw new InvalidOperationException("ISessionFactory não está configurado.");
    }
    return sessionFactory.OpenSession();
});

// Registra os handlers e queries
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CriarPessoaHandler).Assembly);
});

var app = builder.Build();

app.MapPost("/api/pessoas", async (CriarPessoaCommand command, IMediator mediator) =>
{
    var id = await mediator.Send(command);
    return Results.Ok("Registro criado com sucesso!");
});

app.MapGet("/api/pessoas/{id:int}", async (int id, IMediator mediator) =>
{
    var pessoa = await mediator.Send(new BuscarPessoaQuery(id));
    return pessoa != null ? Results.Ok(pessoa) : Results.NotFound();
});

app.MapGet("/api/pessoas", async (IMediator mediator) =>
{
    var pessoa = await mediator.Send(new BuscarPessoasQuery());
    return pessoa != null ? Results.Ok(pessoa) : Results.NotFound();
});

app.Run();