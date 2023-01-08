using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MKFotografiaBackend.Components;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Middleware;
using MKFotografiaBackend.Models;
using MKFotografiaBackend.Services;
using NLog;
using NLog.Web;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
var applicationData = new ApplicationData();
builder.Configuration.GetSection("ApplicationData").Bind(applicationData);
builder.Services.AddSingleton(applicationData);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<MKSeeder>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MKFotografia", Version = "v1" });
    options.AddSecurityDefinition("jwt_auth", new OpenApiSecurityScheme
    {
        Name = "Bearer",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("All", policyBuilder =>
    {
        policyBuilder.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddMvc().AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ISliderPhotoService, SliderPhotoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IFileService, FileService>();

var connectionString = builder.Configuration.GetConnectionString("MKDbConnection");
builder.Services.AddDbContext<MKDbContext>(
    options => {
        options
            .UseMySql(builder.Configuration.GetConnectionString("MKDbConnection"), ServerVersion.AutoDetect(connectionString));
        if (builder.Environment.IsDevelopment())
        {
            options
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableServiceProviderCaching()
                .EnableDetailedErrors();
        }
    }
);

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<MKSeeder>();
app.UseCors("All");
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (applicationData.UseHttps)
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Trace("MKBackend successfully initialized");

await app.RunAsync();
