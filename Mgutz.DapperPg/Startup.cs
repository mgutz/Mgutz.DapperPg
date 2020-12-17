using Mgutz.DapperPg.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mgutz.DapperPg {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            Dal.Startup.Configure(Configuration, services);
            services.AddHttpContextAccessor();
            services.AddScoped(provider => {
                var context = provider.GetRequiredService<IHttpContextAccessor>();

                // TODO extract request context from header value (usually from JWT)
                var token = context?.HttpContext?.Request.Headers["dummy_token"];
                return new RequestContext { UserId = 42 };
            });
            Services.Startup.Configure(Configuration, services);
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
