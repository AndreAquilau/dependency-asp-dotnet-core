using Dapper;
using Microsoft.Data.SqlClient;
using DependencyStore.Models;
using DependencyStore.Repositories.Contracts;

namespace DependencyStore.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly SqlConnection _connection;

    public CustomerRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<Customer?> GetByIdAsync(string customerId)
    {
        // #1 - Recupera o cliente
        var query = $"SELECT [Id], [Name], [Email] FROM CUSTOMER WHERE ID={customerId}";
        return await _connection.QueryFirstOrDefaultAsync<Customer>(query);
    }
}

