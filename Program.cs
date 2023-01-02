using Microsoft.EntityFrameworkCore;
using MKFotografiaBackend;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var applicationData = new ApplicationData();
builder.Configuration.GetSection("ApplicationData").Bind(applicationData);
builder.Services.AddSingleton(applicationData);

builder.Services.AddScoped<MKSeeder>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MKDbConnection");
builder.Services.AddDbContext<MKDbContext>(
    options => {
        options
            .UseMySql(builder.Configuration.GetConnectionString("MKDbConnection"), ServerVersion.AutoDetect(connectionString));
        if (builder.Environment.IsDevelopment())
        {
            options
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableServiceProviderCaching()
                .EnableDetailedErrors();
        }
    }
);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<MKSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (applicationData.UseHttps)
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
