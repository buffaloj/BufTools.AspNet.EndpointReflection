using Microsoft.Extensions.PlatformAbstractions;
using TestWebApi.Requests;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var basePath = PlatformServices.Default.Application.ApplicationBasePath;
var fileName = typeof(ExampleRequest).GetTypeInfo().Assembly.GetName().Name + ".xml";
var XmlCommentsFilePath = Path.Combine(basePath, fileName);
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(XmlCommentsFilePath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();