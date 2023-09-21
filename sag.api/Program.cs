using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using sag.Application.Extensions;
using sag.Infrastructure.Extensions;
using sag.Persistence.Extensions;
using FluentValidation;
using sag.Application.Common.Structs;
using sag.Application.Features.Auth.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

// Add Validator Struct to MediatR
builder.Services.AddValidatorsFromAssemblyContaining(typeof(LoginUserValidator));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(applicationBuilder =>
{
    applicationBuilder.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        context.Response.StatusCode = exceptionHandlerPathFeature?.Error switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = Response<object>.Fail(new ErrorResponse
        {
            Message = exceptionHandlerPathFeature?.Error.Message,
            Exception = app.Environment.IsDevelopment()
                ? exceptionHandlerPathFeature?.Error
                : null
        });

        await context.Response.WriteAsync(JsonSerializer.Serialize(response), Encoding.UTF8);
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();