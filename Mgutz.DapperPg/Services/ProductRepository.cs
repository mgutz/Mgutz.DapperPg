using Dapper;
using Mgutz.DapperPg.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Mgutz.DapperPg.Services {
    public class ProductRepository : BaseRepository, IProductRepository {
        public ProductRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<IEnumerable<Product>> GetAllProducts() {
            var stmt = @"select * from products";

            return await WithConnection(async conn => {
                var query = await conn.QueryAsync<Product>(stmt);
                return query;
            });
        }

        public async ValueTask<Product> GetById(int id) {
            var stmt = @"select * from products where id = @id";

            return await WithConnection(async conn => {
                return await conn.QueryFirstOrDefaultAsync<Product>(stmt, new { Id = id });
            });
        }

        public async ValueTask<Product> AddProduct(Product entity) {
            var stmt = @"
                insert into products (name, cost) 
                values (@name, @cost) 
                returning id, created_at, updated_at
            ";

            return await WithConnection(async conn => {
                var p = await conn.QueryFirstOrDefaultAsync<Product>(stmt, new { Name = entity.Name, Cost = entity.Cost });
                // TODO auto-generate clone method
                entity.Id = p.Id;
                entity.CreatedAt = p.CreatedAt;
                entity.UpdatedAt = p.UpdatedAt;
                return entity;
            });
        }

        public async ValueTask<Product> UpdateProduct(Product entity, int id) {
            var stmt = @"
                update products 
                set name = @name, cost = @cost, updated_at = now() 
                where id = @id 
                returning updated_at
            ";

            return await WithConnection(async conn => {
                var p = await conn.QueryFirstOrDefaultAsync<Product>(stmt,
                    new { Name = entity.Name, Cost = entity.Cost, Id = id });
                // TODO auto-generate clone method
                entity.Id = id;
                entity.UpdatedAt = p.UpdatedAt;
                entity.CreatedAt = p.CreatedAt;
                return entity;
            });

        }

        public async Task RemoveProduct(int id) {
            var stmt = @"delete from products where id = @id";

            await WithConnection(async conn => {
                await conn.ExecuteAsync(stmt, new { Id = id });
            });
        }
    }
}
