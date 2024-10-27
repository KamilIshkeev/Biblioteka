//using Biblioteka.DatabContext;
//using Biblioteka.Interfaces;
//using Biblioteka.Services;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<IGenreService, GenreService>();
//builder.Services.AddScoped<IReaderService, ReaderService>();
//builder.Services.AddScoped<IRentalService, RentalService>();

//builder.Services.AddDbContext<BiblioApiDB>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioDbString")), ServiceLifetime.Scoped);



//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();








using Microsoft.EntityFrameworkCore;
using System.Text;
using Biblioteka.Services;
using Microsoft.IdentityModel.Tokens;
using ProxyKit;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Builder;
using Biblioteka.Interfaces;
using Biblioteka.DatabContext;
using Biblioteka.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IRentalService, RentalService>();
//builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddDbContext<BiblioApiDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioDBString")), ServiceLifetime.Scoped);

builder.Services.AddProxy();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});

builder.Services.AddProxy();
var app = builder.Build();

// app.UseAuthentication();
// app.UseAuthorization();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseCors("CorsPolicy");


//app.UseWhen(context => context.Request.Path.Value.Contains("/api/Books"),
//    applicationBuilder => applicationBuilder.RunProxy(context =>
//        context.ForwardTo("http://localhost:5136").AddXForwardedHeaders().Send()));
//app.UseWhen(context => context.Request.Path.Value.Contains("/api/Genres"),
//    applicationBuilder => applicationBuilder.RunProxy(context =>
//        context.ForwardTo("http://localhost:5136").AddXForwardedHeaders().Send()));
//app.UseWhen(context => context.Request.Path.Value.Contains("/api/Rent"),
//    applicationBuilder => applicationBuilder.RunProxy(context =>
//        context.ForwardTo("http://localhost:5136").AddXForwardedHeaders().Send()));
app.UseWhen(context => context.Request.Path.Value.Contains("/api/Readers"),
    applicationBuilder => applicationBuilder.RunProxy(context =>
        context.ForwardTo("https://localhost:7134").AddXForwardedHeaders().Send()));



app.MapControllers();

app.Run();


