using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyCorreios.Controllers;

[ApiController]
[Route("[controller]")]
public class ShippingController : ControllerBase
{
    [HttpGet("{trackingCode}")]
    async public Task<string> Get(string trackingCode)
    {
        
        string urlTracking = "https://proxyapp.correios.com.br/v1/sro-rastro/";

        var client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync(urlTracking + trackingCode);

        return await response.Content.ReadAsStringAsync();

    }
}
