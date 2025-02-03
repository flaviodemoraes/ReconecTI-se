using EfDbExistente.Infrastructure.Data;
using EfDbExistente.Repositories;
using EfDbExistente.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados pegando do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// REGISTRAR O ProdutoService NO CONTÊINER DE DEPENDÊNCIAS
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Endpoints para Produtos
app.MapGet("/produtos", async ([FromServices] ProdutoService service) =>
    Results.Ok(await service.ListarProdutosDTO()));

app.MapGet("/produtos/{id:int}", async (int id, [FromServices] ProdutoService service) =>
{
    var produto = await service.ObterProdutoDTO(id);
    return produto is not null ? Results.Ok(produto) : Results.NotFound();
});

app.MapPost("/produtos", async ([FromBody] Produto produto, [FromServices] ProdutoService service) =>
{
    var model = new Produto(produto.Nome, produto.CategoriaId, produto.Quantidade, produto.Preco);
    await service.AdicionarProduto(model);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/produtos/{id:int}", async (int id, [FromBody] Produto produto, [FromServices] ProdutoService service) =>
{
    var produtoDb = await service.ObterProdutoDTO(id);
    if (produtoDb is null) return Results.NotFound();

    await service.AtualizarProduto(produto);
    return Results.NoContent();
});

app.MapPut("/produtos/desativar/{id:int}", async (int id, [FromServices] ProdutoService service) =>
{
    var produtoDb = await service.ObterProduto(id);
    if (produtoDb is null) return Results.NotFound();
    
    // Desativar produto.
    produtoDb.DesativarProduto();

    await service.AtualizarProduto(produtoDb);
    return Results.NoContent();
});

app.MapPut("/produtos/entradaEstoque/{id:int}", async (int id, decimal quantidadeEntrada, [FromServices] ProdutoService service) =>
{
    var produtoDb = await service.ObterProduto(id);
    if (produtoDb is null) return Results.NotFound();

    // Entrada de estoque.
    produtoDb.EntradaQuantidade(quantidadeEntrada);

    await service.AtualizarProduto(produtoDb);
    return Results.NoContent();
});

app.MapPut("/produtos/baixaEstoque/{id:int}", async (int id, decimal quantidadeEntrada, [FromServices] ProdutoService service) =>
{
    var produtoDb = await service.ObterProduto(id);
    if (produtoDb is null) return Results.NotFound();

    // Baixa de estoque.
    produtoDb.BaixarQuantidade(quantidadeEntrada);

    await service.AtualizarProduto(produtoDb);
    return Results.NoContent();
});

app.MapDelete("/produtos/{id:int}", async (int id, [FromServices] ProdutoService service) =>
{
    var produto = await service.ObterProduto(id);
    if (produto is null) return Results.NotFound();

    await service.DeletarProduto(id);
    return Results.NoContent();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();