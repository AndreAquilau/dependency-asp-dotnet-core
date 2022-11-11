using RestSharp;
using DependencyStore;
using DependencyStore.Services.Contracts;
namespace DependencyStore.Services;

public class DeliveryFeeService : IDeliveryFeeService
{
    private readonly Configuration _configuration;

    public DeliveryFeeService(Configuration configuration)
    {
        _configuration = configuration;
    }

    public async Task<decimal> GetDeliveryFeeAsync(string zipCode)
    {
        decimal deliveryFee = 0;
        var client = new RestClient(_configuration.DeliveryFeeService);
        var request = new RestRequest()
            .AddJsonBody(new
            {
                zipCode
            });

        deliveryFee = await client.PostAsync<decimal>(request, new CancellationToken());
        
        // Nunca é menos que R$ 5,00
        return deliveryFee < 5 ? 5 : deliveryFee;
    }
}