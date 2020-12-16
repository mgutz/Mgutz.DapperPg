using Mgutz.DapperPg.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using Npgsql;

namespace Mgutz.DapperPg {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            ColumnMapper.Initialize();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddMvc();

            services.AddOpenApiDocument(config => {
                config.Title = "ASPNET CORE 5/WebAPI/Dapper Async/PostgreSQL Prototype";
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
