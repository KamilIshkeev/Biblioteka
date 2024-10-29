using BooksService.DatabContext;
using BooksService.Interfaces;
using BooksService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookService, BookService>();



//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder =>
//        {
//            builder.WithOrigins("https://localhost:7209;http://localhost:5278")
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

builder.Services.AddDbContext<BookDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookDbConnection")), ServiceLifetime.Scoped);
//builder.Services.AddDbContext<PhotoDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoDbConnection")), ServiceLifetime.Scoped); // Replace with your connection string




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();