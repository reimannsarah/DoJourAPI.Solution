using DoJourAPI.Models;
using Microsoft.EntityFrameworkCore;
using DoJourAPI.Services;
using DoJourAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DoJourAPIContext>(
                    dbContextOptions => dbContextOptions
                        .UseMySql(
                        builder.Configuration["ConnectionStrings:AZURE_MYSQL_CONNECTIONSTRING"], 
                        ServerVersion.AutoDetect(
                            builder.Configuration["ConnectionStrings:AZURE_MYSQL_CONNECTIONSTRING"]
                        )
                        )
                    );

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });                  

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(builder => 
    {
        builder.AllowAnyHeader()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IEntryService, EntryService>();
builder.Services.AddScoped<IEntryRepository, EntryRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();