using Dapper;
using Microsoft.Data.SqlClient;
using DependencyStore.Models;
using DependencyStore.Repositories.Contracts;

namespace DependencyStore.Repositories;

public class PromoCodeRepository : IPromoCodeRepository
{
    private readonly SqlConnection _connection;

    public PromoCodeRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<PromoCode?> GetPromoCodeAsync(string promoCode)
    {
        decimal discount = 0;

        var query = $"SELECT * FROM PROMO_CODES WHERE CODE={promoCode}";
        var promo = await _connection.QueryFirstAsync<PromoCode>(query);
        if (promo.ExpireDate > DateTime.Now)
        {
            discount = promo.Value;
        }

        return promo;
    }
}