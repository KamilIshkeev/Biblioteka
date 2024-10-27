//using ReaderServ.DatabContext;
//using ReaderServ.Interfaces;
//using ReaderServ.Services;
//using Microsoft.EntityFrameworkCore;
//using ReaderServ.DatabContext;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<IReaderService, ReaderService>();
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Add services to the container.
//builder.Services.AddDbContext<ReaderDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ReaderDbConnection"))); //Replace with your connection string




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












using System.Text;
using ReaderServ.DatabContext;
using ReaderServ.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReaderServ.Interfaces;
using Microsoft.AspNetCore.CookiePolicy;
using ProxyKit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            // указывает, будет ли валидироваться издатель при валидации токена
//            ValidateIssuer = true,
//            // строка, представляющая издателя
//            ValidIssuer = AuthOptions.ISSUER,
//            // будет ли валидироваться потребитель токена
//            ValidateAudience = true,
//            // установка потребителя токена
//            ValidAudience = AuthOptions.AUDIENCE,
//            // будет ли валидироваться время существования
//            ValidateLifetime = true,
//            // установка ключа безопасности
//            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//            // валидация ключа безопасности
//            ValidateIssuerSigningKey = true,
//        };
//    });

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReaderService, ReaderService>();

builder.Services.AddProxy();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddDbContext<ReaderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReaderDbConnection")), ServiceLifetime.Scoped);






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();











//using Microsoft.EntityFrameworkCore;
//using ReaderServ.DatabContext;
//using ReaderServ.Interfaces;
//using ReaderServ.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddScoped<IReaderService, ReaderService>();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ReaderDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ReaderDbConnection")));


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
