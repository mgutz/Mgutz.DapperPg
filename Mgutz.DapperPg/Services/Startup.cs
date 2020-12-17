using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mgutz.DapperPg.Services {

    public class Startup {
        /// <summary>Configure registers DI services.</summary>
        public static void Configure(IConfiguration _, IServiceCollection services) {
            services.AddScoped<IProductService, ProductService>();
        }
    }

}
