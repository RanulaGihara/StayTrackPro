using Microsoft.EntityFrameworkCore; 
using StayTrackPro.API.Models;        

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

// Register DbContext with correct key
builder.Services.AddDbContext<StayTrackProDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StayTrackProDb")));


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