using eCommerece.API.Infrastruture;
using eCommerece.API.Core;
using eCommerece.API.Middleware;
using System.Text.Json.Serialization;
using eCommerece.API.Core.Mapper;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastruture();
builder.Services.AddCore();

builder.Services.AddCors(options =>{
    options.AddDefaultPolicy(builder =>{
        builder.WithOrigins("http://localhost:5196").AllowAnyMethod().AllowAnyHeader();
    });
});



builder.Services.AddControllers().AddJsonOptions(options => {options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());});
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

builder.Services.AddFluentValidationAutoValidation();
//Add API explorer Services
builder.Services.AddEndpointsApiExplorer();
//Add swagger genration services to create specification
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.useExceptionHandlingMiddleware();

app.UseRouting();
//Add Endpoint that server wagger.json
app.UseSwagger();
// Add endpoint to see in Ui
app.UseSwaggerUI();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

//Controller Routes
app.MapControllers();

app.Run();
