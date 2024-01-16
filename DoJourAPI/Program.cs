using DoJourAPI.Models;
using Microsoft.EntityFrameworkCore;
using DoJourAPI.Services;
using DoJourAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.dot

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
