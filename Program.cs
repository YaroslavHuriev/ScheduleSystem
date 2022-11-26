using System.Data;

using Npgsql;

using ScheduleSystem.Application.Handlers.ScheduleInputListQueryHandler;
using ScheduleSystem.Application.Handlers.ScheduleInputQueryHandler;
using ScheduleSystem.Application.Handlers.ScheduleLessonsByScheduleIdQueryHandler;
using ScheduleSystem.Application.UseCases;
using ScheduleSystem.Application.UseCases.CreateInputDataUseCase;
using ScheduleSystem.Infrastructure;
using ScheduleSystem.Infrastructure.LessonTimeRepository;
using ScheduleSystem.Infrastructure.Options;
using ScheduleSystem.Infrastructure.ScheduleRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(new MongoOptions(
	Environment.GetEnvironmentVariable("MONGODB_CONN_STRING"),
	Environment.GetEnvironmentVariable("SCHEDULE_DB_NAME"),
	Environment.GetEnvironmentVariable("SCHEDULE_INPUT_COLLECTION_NAME")));
builder.Services.AddTransient<IScheduleInputRepository, ScheduleInputRepository>();
builder.Services.AddTransient<IScheduleInputListQueryHandler, ScheduleInputListQueryHandler>();
builder.Services.AddTransient<IScheduleInputQueryHandler, ScheduleInputQueryHandler>();
builder.Services.AddTransient<IScheduleLessonsByScheduleIdQueryHandler, ScheduleLessonsByScheduleIdQueryHandler>();
builder.Services.AddTransient<ILessonTimeRepository, LessonTimeRepository>();
builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();
builder.Services.AddTransient<IGenerateScheduleUseCase, GenerateScheduleUseCase>();
builder.Services.AddTransient<ICreateInputDataUseCase, CreateInputDataUseCase>();
builder.Services.AddTransient<ILessonsListQueryHandler, LessonsListQueryHandler>();
builder.Services.AddTransient<ILessonRepository, LessonRepository>();
builder.Services.AddTransient<IDeleteInputDataUseCase, DeleteInputDataUseCase>();
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
