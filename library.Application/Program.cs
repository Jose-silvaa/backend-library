using System.Text;
using library.Domain.Domain.Book.Read.Repositories;
using library.Domain.Domain.Book.Write.Commands;
using library.Domain.Domain.Book.Write.Repositories;
using library.Domain.Domain.Category.Read.Repositories;
using library.Domain.Domain.Category.Write.Repositories;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Loan.Read.Repositories;
using library.Domain.Domain.Loan.Write.Repositories;
using library.Domain.Domain.User.Read.Model;
using library.Domain.Domain.User.Read.Repositories;
using library.Domain.Domain.User.Write.Commands;
using library.Domain.Domain.User.Write.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A variável de ambiente DEFAULT_CONNECTION não está definida.");
}

// Configura o DbContext usando a connection string da variável de ambiente
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());

builder.Services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();

builder.Services.AddScoped<IUserWriteRepository, UserWriteRepository>();
builder.Services.AddScoped<IUserReadRepository,  UserReadRepository>();

builder.Services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
builder.Services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();

builder.Services.AddScoped<IBookWriteRepository, BookWriteRepository>();
builder.Services.AddScoped<IBookReadRepository, BookReadRepository>();

builder.Services.AddScoped<ILoanWriteRepository, LoanWriteRepository>();
builder.Services.AddScoped<ILoanReadRepository, LoanReadRepository>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddAuthorization();
builder.Services.AddControllers();


var app = builder.Build();


app.UseCors("AllowFrontend");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapMethods("{*path}", new[] { "OPTIONS" }, context =>
{
    context.Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
    context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
    context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.StatusCode = 200;
    return Task.CompletedTask;
});

app.Run();
