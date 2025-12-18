using System;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddControllers();
builder.Services.AddCors(options => 
    options.AddPolicy("AllowAll", 
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseCors("AllowAll");
app.MapControllers();

// Endpoints do sistema
app.MapGet("/", () => "🚀 TechFuture CRM API - Vendas e Atendimento");
app.MapGet("/api/status", () => new {
    sistema = "TechFuture CRM",
    modulo = "Vendas e Atendimento ao Cliente",
    equipe = 3,
    status = "online",
    versao = "1.0",
    data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
});

app.MapGet("/api/dashboard", () => new {
    totalClientes = 15,
    totalPedidosMes = 8,
    receitaMes = 45999.90,
    cotacoesPendentes = 3,
    chamadosAbertos = 2
});

app.MapGet("/api/clientes", () => {
    var clientes = new[] {
        new { id = 1, nome = "João Silva", email = "joao@email.com", telefone = "(11) 9999-8888", status = "Ativo" },
        new { id = 2, nome = "Maria Oliveira", email = "maria@email.com", telefone = "(21) 9777-6666", status = "Ativo" },
        new { id = 3, nome = "Tech Solutions LTDA", email = "contato@tech.com", telefone = "(11) 3333-4444", status = "Ativo" }
    };
    return Results.Ok(new { 
        mensagem = "Clientes cadastrados",
        total = clientes.Length,
        dados = clientes 
    });
});

app.MapPost("/api/clientes", (dynamic cliente) => {
    return Results.Ok(new {
        mensagem = "Cliente criado com sucesso!",
        id = new Random().Next(100, 999),
        cliente = cliente,
        dataCriacao = DateTime.Now,
        status = "Ativo"
    });
});

Console.WriteLine("✅ API TechFuture CRM iniciada em http://localhost:5000");
Console.WriteLine("📊 Dashboard: http://localhost:5000/api/dashboard");
Console.WriteLine("👥 Clientes: http://localhost:5000/api/clientes");

app.Run();
