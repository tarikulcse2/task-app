using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Test.Entities.DBContext;
using Test.Repository;
using Test.Service;
using Test.WebApi.Config;
using Test.WebApi.Filters;

namespace Test.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<TestDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("TestDBConnection")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            // service register
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJobService, JobService>();

            // filter 
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomModelStateValidatorFilter));
                options.ModelBinderProviders.Insert(0, new JsonModelBinderProvider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(options => options
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        );
            app.Use(async (context, next) =>
            {
                await next?.Invoke();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next?.Invoke();
                }
            });

            // register authentication
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc();
        }
    }
}
