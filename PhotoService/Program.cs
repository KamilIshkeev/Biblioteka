using PhotoService.Interfaces;
using Microsoft.EntityFrameworkCore;
using PhotoService.Data;
using PhotoService.Services;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPhotoService, PhotoService.Services.PhotoService>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//Add this line to enable file uploads
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Add DbContext (CRUCIAL - DOUBLE CHECK THIS CONNECTION STRING)
builder.Services.AddDbContext<PhotoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoDbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();//Enable Static Files for Swagger UI


// Use CORS
app.UseCors();

app.MapControllers();

app.Run();













//using Microsoft.EntityFrameworkCore;
//using PhotoService.Data;
//using PhotoService.Interfaces;
//using PhotoService.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IPhotoService, PhotoService.Services.PhotoService>(); //Corrected path

////Add this line to enable file uploads
//builder.Services.AddMvc().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = null;
//});

//// Add DbContext (CRUCIAL - DOUBLE CHECK THIS CONNECTION STRING)
//builder.Services.AddDbContext<PhotoDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoDbConnection")));


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection(); //Consider removing this in development for easier testing

//app.UseAuthorization();
//app.UseStaticFiles();//Enable Static Files for Swagger UI

//app.MapControllers();

//app.Run();














//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.OpenApi.Models;
//using PhotoService.Data;
//using PhotoService.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IPhotoService, PhotosService>();


//// Add DbContext !!!
//builder.Services.AddDbContext<PhotoDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoDbConnection")));


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();










//using Microsoft.EntityFrameworkCore;
//using PhotoService.Data;
//using PhotoService.Interfaces;
//using PhotoService.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Database connection (replace with your connection string) !!!
//builder.Services.AddDbContext<PhotoDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoDbConnection")));

//builder.Services.AddScoped<IPhotoService, PhotoServiceImplementation>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();

