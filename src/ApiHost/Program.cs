using System.Reflection;
using Application;
using Application.Mapper;
using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Postgres;
using Infrastructure.Postgres.Persistence;
using Microsoft.EntityFrameworkCore;
using Presentation.API.V1.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    var connectionString = Environment.GetEnvironmentVariable("POSTGRES_DB_CONNECTION_STRING") 
                           ?? throw new InvalidOperationException("Не найдена строка подключения к PostgreSQL в переменных окружения (POSTGRES_DB_CONNECTION_STRING)");
    options.UseNpgsql(connectionString);
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddInfrasctructure();
builder.Services.AddApplication();

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ExceptionFilter>();
    })
    .AddApplicationPart(typeof(Presentation.API.V1.AssemblyReference).Assembly);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidators();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{typeof(Presentation.API.V1.AssemblyReference).Assembly.GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

