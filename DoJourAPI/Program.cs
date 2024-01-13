using DoJourAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.dot

builder.Services.AddControllers();

builder.Services.AddDbContext<DoJourAPIContext>(
                    dbContextOptions => dbContextOptions
                        .UseMySql(
                        builder.Configuration["ConnectionStrings:DefaultConnection"], 
                        ServerVersion.AutoDetect(
                            builder.Configuration["ConnectionStrings:DefaultConnection"]
                        )
                        )
                    );

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(builder => 
    {
        builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
