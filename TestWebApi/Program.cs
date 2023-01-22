using Microsoft.Extensions.PlatformAbstractions;
using Reflectamundo.TestWebApi.Requests;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var basePath = PlatformServices.Default.Application.ApplicationBasePath;
var fileName = typeof(ExampleRequest).GetTypeInfo().Assembly.GetName().Name + ".xml";
var XmlCommentsFilePath = Path.Combine(basePath, fileName);
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(XmlCommentsFilePath);
});

var app = builder.Build();

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