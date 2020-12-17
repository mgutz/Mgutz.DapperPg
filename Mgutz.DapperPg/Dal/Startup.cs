using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;


namespace Mgutz.DapperPg.Dal {

    public class Startup {
        /// <summary>Configure registers DI services.</summary>
        public static void Configure(IConfiguration config, IServiceCollection services) {
            ColumnMapper.Initialize();
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }

}
