using Microsoft.AspNetCore.Mvc;
using ProvaA2HerosHlatkiAPI.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();



app.MapPost("funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDbContext context) =>
{
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(
            funcionario, new ValidationContext(funcionario), erros, true))
    {
        return Results.BadRequest(erros);
    }

    Funcionario? funcionarioBuscado = context.Funcionarios.FirstOrDefault(f => f.CPF == funcionario.CPF);

    if(funcionarioBuscado == null)
    {
        context.Funcionarios.Add(funcionario);
        context.SaveChanges();
        return Results.Created();
    }
    return Results.BadRequest("Já existe um funcionário com este CPF!");
});

app.MapGet("/funcionario/listar", ([FromServices] AppDbContext context) =>
{
    if(context.Funcionarios.Any())
    {
        return Results.Ok(context.Funcionarios.ToList());
    }
    return Results.NotFound("Não existem funcionários na tabela!");
});

app.MapPost("folha/cadastrar", (Folha folha, [FromServices] AppDbContext context ) =>
{
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(
            folha, new ValidationContext(folha), erros, true))
    {
        return Results.BadRequest(erros);
    }

    Folha? folhaBuscada = context.Folhas.FirstOrDefault(f => f.Id == folha.Id);

    if (folhaBuscada == null)
    {
        context.Folhas.Add(folha);
        context.SaveChanges();
        return Results.Created();
    }
    return Results.BadRequest("Já existe uma folha com este Id");
});

app.MapGet("folha/listar", ([FromServices] AppDbContext context) =>
{
    if(context.Folhas.Any())
    {
        return Results.Ok(context.Folhas.ToList());
    }
    return Results.NotFound("Não existem folhas na tabela");
});

app.Map("folha/buscar/{cpf}/{mes}/{ano}", (string cpf, int mes, int ano, [FromServices] AppDbContext context) =>
{
        Funcionario? funcionarioPorCpf = context.Funcionarios.FirstOrDefault(f => f.CPF == cpf);
        if(funcionarioPorCpf is null)
        {
            return Results.NotFound("Funcionário não foi encontrado pelo CPF digitado! Certifique-se de que o formato do CPF esteja em 123.123.123-12");
        }
        Folha? folha = context.Folhas.FirstOrDefault(f => f.FuncionarioId == funcionarioPorCpf.Id && funcionarioPorCpf.CPF == cpf && f.Mes == mes && f.Ano == ano);
        return Results.Ok(folha);
       
});

app.Run();
