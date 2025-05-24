using System.Text.Json.Serialization;
using BusinessLogicLayer;
using DataAccessLayer;
using FluentValidation.AspNetCore;
using Product.API.APIEndpoints;
using Product.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);

builder.Services.AddCors(options =>{
    options.AddDefaultPolicy(builder =>{
        builder.WithOrigins("http://localhost:5196").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>{
options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionMiddleware();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapProductAPIEndpoints();
app.UseCors();
app.MapGet("/", () => "Hello World!");

app.Run();
