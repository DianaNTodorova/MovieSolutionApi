using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MovieApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieApiContext")
        ?? throw new InvalidOperationException("Connection string 'MovieApiContext' not found.")));


builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MapperProfile>();
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.SeedData();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
