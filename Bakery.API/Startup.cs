using Bakery.DataAccess.Database;
using Bakery.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bakery.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment? CurrentEnvironment { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthorization();
            services.AddAuthentication();

            services.AddDbContext<BakeryDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BakeryConnectionString"));
            });

            #region AutoMapper
            MapperConfig.AutoMapperConfiguration.Configure();
            #endregion

            #region Database
            services.AddScoped(typeof(DbContext));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Bakery API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CurrentEnvironment = env;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Bakery API V1");
                s.RoutePrefix = string.Empty;
            });

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BakeryDbContext>();
                context!.Database.Migrate();
            }

        }
    }
}
