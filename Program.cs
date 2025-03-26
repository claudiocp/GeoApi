using GeoApi.Data;
using GeoApi.Models;
using GeoApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adicionar política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Adicionar serviços ao container
builder.Services.AddControllers();

// Configurar a conexão com o banco de dados
builder.Services.AddDbContext<GeoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar serviços
builder.Services.AddScoped<IGeoService, GeoService>();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "GeoAPI", 
        Version = "v1",
        Description = "API para georreferenciamento de estados e cidades",
        Contact = new OpenApiContact
        {
            Name = "Desenvolvedor",
            Email = "dev@exemplo.com"
        }
    });
    
    // Configure Swagger para ler os comentários XML para documentação
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoAPI v1"));
}

app.UseHttpsRedirection();
// Adicionar middleware CORS
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
