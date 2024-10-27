
//using PhotoService.Interfaces;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;
//using PhotoService.Data;
//using PhotoService.Services;


//namespace PhotoService.Requests
//{
//    public class CreatePhotos
//    {
//        public CreatePhotos(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

       
       
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddScoped<IPhotoService, PhotoServiceImplementation>();
//            services.AddDbContext<PhotoDbContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("PhotoDbString")), ServiceLifetime.Scoped); 
            
//            services.AddControllers();

//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlinePhotoAPI", Version = "v1" });
//                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                {
//                    In = ParameterLocation.Header,
//                    Description = "Введите 'Bearer' [пробел] и затем ваш токен JWT",
//                    Name = "Authorization",
//                    Type = SecuritySchemeType.ApiKey
//                });
//                c.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                        new OpenApiSecurityScheme
//                        {
//                            Reference = new OpenApiReference
//                            {
//                                Type = ReferenceType.SecurityScheme,
//                                Id = "Bearer"
//                            }
//                        },
//                        new string[] { }
//                    }
//                });
//            });

//            services.AddCors(options =>
//            {
//                options.AddPolicy("CorsPolicy",
//                    builder => builder.WithOrigins("http://localhost:5169/api/Photos/upload", "https://ya.ru") // Replace with YOUR frontend domains
//                                       .AllowAnyMethod()
//                                       .AllowAnyHeader()
//                                       .AllowCredentials()); // If you need to send cookies
//            });
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseRouting();
//            app.UseCors("CorsPolicy"); // Moved here
//            app.UseAuthentication();
//            app.UseAuthorization();


//            app.UseSwagger();
//            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineLibraryAPI v1"));

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
           
//        }

//    }
//}
