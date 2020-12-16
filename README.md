# Mgutz.DapperPg

A .NET 5 core WebApi project using PostgreSQL and Dapper. No Entity Framework. Straight up SQL.

Tested on Debian (linux).

## Technologies

- .NET 5 WebApi
- Dapper
- PostgreSQL

## Testing

In a terminal start postgres

```sh
docker-compose up
```

In another terminal, run the project

```sh
# first DB run migrations with your own utility
dat up

# run server
dotnet run --project Mgutz.DapperPg
```

Browse `http://localhost:5000/swagger` to interact with the API through swagger
UI.

### Interact with API via Terminal

```sh
# create a product
curl -H 'Content-Type: application/json' -d '{"name": "Apple", "cost": 0.50}' http://localhost:5000/api/product -v

# list products
curl http://localhost:5000/api/product

# get product id=1000
curl http://localhost:5000/api/product/1000

```

## Notes

- No need to use using statement. Dapper will automatically open, close and dispose of the connection.

## Credit

Forked from [berkayyerdelen/Dapper.Webapi](https://github.com/berkayyerdelen/Dapper.WebApi)

## LICENSE

MIT Licensed.
