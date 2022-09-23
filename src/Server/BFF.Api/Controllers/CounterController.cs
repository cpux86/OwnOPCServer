using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;

namespace BFF.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetCurrentMeterReadings()
        {
            var t = TimeSpan.FromSeconds(5);
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5501");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "service",
                ClientSecret = "client_secret_service",
                Scope = "ServiceApi"
            });
            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}
            var accessToken = tokenResponse.AccessToken;

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            return await httpClient.GetStringAsync("https://localhost:5601/api/Counter/GetCurrentMeterReadings");
            
        }
    }
}
