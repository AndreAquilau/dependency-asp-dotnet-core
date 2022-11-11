using DependencyStore.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("Default");

builder.Services.AddConfigurations();
builder.Services.AddSqlConnection("CONN_STR");
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers();

var app = builder.Build(); 

app.MapControllers();

app.Run();