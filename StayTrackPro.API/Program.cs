using Microsoft.EntityFrameworkCore;
using StayTrackPro.API.Models;
using StayTrackPro.API.Repositories;
using StayTrackPro.API.Repositories.Interfaces;
using StayTrackPro.API.Services;
using StayTrackPro.API.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

// Register DbContext
builder.Services.AddDbContext<StayTrackProDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StayTrackProDb")));

// Register Repositories & Services for DI
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
