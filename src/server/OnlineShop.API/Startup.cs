using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OnlineShop.API.ConfigurationOptions;
using OnlineShop.Data;
using OnlineShop.Data.Infrastructure.Core;
using OnlineShop.Models;
using OnlineShop.Services;
using OnlineShop.Services.BaseServices;

namespace OnlineShop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            AppSettings = new AppSettings();
            Configuration.Bind(AppSettings);
        }

        private AppSettings AppSettings { get; set; }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);

            services.AddDbContext<OnlineShopDbContext>(options =>
                options.UseSqlServer(AppSettings.ConnectionStrings.OnlineShopConn));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineShop.API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins", builder => builder
                    .WithOrigins(AppSettings.CORS.AllowedOrigins)
                    .WithHeaders(AppSettings.CORS.AllowedHeadersList)
                    .WithMethods(AppSettings.CORS.AllowedMethodsList));

                options.AddPolicy("AllowAnyOrigin", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                options.AddPolicy("CustomPolicy", builder => builder
                    .AllowAnyOrigin()
                    .WithMethods("Get")
                    .WithHeaders("Content-Type"));
            });


            services.AddScoped<ICoreRepository<Category>, CoreRepository<Category>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseService<Category>, BaseServices<Category>>();

            services.AddScoped<ICategoryService, CategoryServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShop.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AppSettings.CORS.AllowAnyOrigin ? "AllowAnyOrigin" : "AllowedOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
