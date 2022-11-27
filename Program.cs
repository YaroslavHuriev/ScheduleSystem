using System.Data;

using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddUseCases();
builder.Services.AddQueryHandlers();
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(Environment.GetEnvironmentVariable("POSTGRES_CONN_STRING")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
