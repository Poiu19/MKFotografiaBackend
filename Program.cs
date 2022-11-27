using Microsoft.EntityFrameworkCore;
using MKFotografiaBackend;
using MKFotografiaBackend.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
