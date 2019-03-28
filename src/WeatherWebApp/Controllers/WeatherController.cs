using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace WeatherWebApp.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5003/api/weather");
            requestMessage.Headers.Add("Authorization", $"Bearer {await this.HttpContext.GetTokenAsync("access_token")}");

            var weatherResponse = await httpClient.SendAsync(requestMessage);
            if (!weatherResponse.IsSuccessStatusCode)
            {
                return View();
            }

            var weather = await weatherResponse.Content.ReadAsAsync<Weather>();
            
            return View(weather);
        }
    }
}
