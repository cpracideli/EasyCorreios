using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyCorreios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackingController : ControllerBase
{
    [HttpGet("{trackingCode}")]
    async public Task<string> Get(string trackingCode)
    {
        
        string urlTracking = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx?";
        

        var client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync(urlTracking + trackingCode);

        return await response.Content.ReadAsStringAsync();

    }
}
