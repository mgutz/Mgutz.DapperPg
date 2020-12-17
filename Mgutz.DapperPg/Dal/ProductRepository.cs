using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mgutz.DapperPg.Models;

namespace Mgutz.DapperPg.Dal {

    public class ProductRepository : IProductRepository {

        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection conn) {
            _dbConnection = conn;
        }

        public async Task<IEnumerable<Product>> GetAll() {
            var stmt = @"select * from products";

            return await _dbConnection.QueryAsync<Product>(stmt);
        }

        public async ValueTask<Product> GetById(int id) {
            var stmt = @"select * from products where id = @id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(stmt, new { Id = id });
        }

        public async ValueTask<Product> Add(Product entity) {
            var stmt = @"
                insert into products (name, cost) 
                values (@name, @cost) 
                returning id, created_at, updated_at
            ";

            var p = await _dbConnection.QueryFirstOrDefaultAsync<Product>(stmt, new { Name = entity.Name, Cost = entity.Cost });
            // TODO auto-generate clone method
            entity.Id = p.Id;
            entity.CreatedAt = p.CreatedAt;
            entity.UpdatedAt = p.UpdatedAt;
            return entity;
        }

        public async ValueTask<Product> Update(Product entity, int id) {
            var stmt = @"
                update products 
                set name = @name, cost = @cost, updated_at = now() 
                where id = @id 
                returning updated_at
            ";

            var p = await _dbConnection.QueryFirstOrDefaultAsync<Product>(
                stmt,
                new { Name = entity.Name, Cost = entity.Cost, Id = id }
            );
            // TODO auto-generate clone method
            entity.Id = id;
            entity.UpdatedAt = p.UpdatedAt;
            entity.CreatedAt = p.CreatedAt;
            return entity;
        }

        public async Task Remove(int id) {
            var stmt = @"delete from products where id = @id";

            await _dbConnection.ExecuteAsync(stmt, new { Id = id });
        }

    }

}
