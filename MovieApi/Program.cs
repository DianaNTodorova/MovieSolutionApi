using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie.Core.Domain.Contracts;
using Movie.Data.Repositories;
using MovieApi.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext"),
        b => b.MigrationsAssembly("Movie.Data")));


builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IMovieActorRepository, MovieActorRepository>();
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();


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
