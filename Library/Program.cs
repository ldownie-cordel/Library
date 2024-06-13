using Microsoft.EntityFrameworkCore;
using Library.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//uncomment cors stuff and fix the error if you want to use the .NET backend we built instead of the JSON one
// Add services to the container.

// builder.Services.AddCors(x => {
//     x.AddPolicy("CorsPolicy", opts => { opts.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod(); });
// });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddDbContext<BookContext>(opt =>
    opt.UseInMemoryDatabase("BookList"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

 //app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
