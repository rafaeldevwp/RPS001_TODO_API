using App.Profiles;
using Data.Contexto;
using Data.Repository.TarefaRepository;
using Dominio.Core.Data;
using Dominio.Core.Services.Notificador;
using Dominio.Core.Services.TarefasService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using App.Configuration;
using App.Extensions;
using Microsoft.AspNetCore.Http;
using Dominio.Core.User;
using TestesApp.Config;
using App.Controllers;
using System.Security.Claims;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<TarefaContexto>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.IdentityConfigurationService(Configuration);
            services.AddMvc();
            services.AddAutoMapper(typeof(TarefaProfile));
            services.AddScoped<TarefaContexto>();
            services.AddScoped<AppSettings>();
            services.AddScoped<ClaimsIdentity>();
            services.AddTransient<ITarefaService,TarefaService>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AppUser>();
       

            //Injeções de dependencia de testes
            //services.ConfigTests();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App v1"));
            }
            app.UseAuthentication();
            app.UseRouting();

            app.UseCors(
                x => { 
                    x.AllowAnyHeader();
                    x.AllowAnyOrigin();
                });
           
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
