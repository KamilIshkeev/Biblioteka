using PhotoService.DatabContext;
using PhotoService.Interfaces;
using PhotoService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PhotoDatabase");

// Register PhotoService with the string dependency


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IPhotoInterface>(provider =>
//{
//    var context = provider.GetRequiredService<PhotoDbContext>();

//    var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "img");

//    return new PhotosService(storagePath, context);
//});


//builder.Services.AddScoped<IPhotoInterface, PhotosService>();



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

builder.Services.AddDbContext<PhotoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PhotosDbConnection")), ServiceLifetime.Scoped);
//builder.Services.AddDbContext<PhotoDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoDbConnection")), ServiceLifetime.Scoped); // Replace with your connection string

builder.Services.AddHttpContextAccessor();


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