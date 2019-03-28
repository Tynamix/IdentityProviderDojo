using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using Newtonsoft.Json;
using WeatherApi.Business.Contracts;
using WeatherApi.Configuration;

namespace WeatherApi.Business
{
    public class WeatherLogic:IWeatherLogic
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OpenWeatherApiOptions _openWeatherOptions;

        public WeatherLogic(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherApiOptions> openWeatherOptions)
        {
            _httpClientFactory = httpClientFactory;
            _openWeatherOptions = openWeatherOptions.Value;
        }

        /// <summary>
        /// Gets the temperature of current users location
        /// </summary>
        /// <param name="currentUser">The currently logged in user</param>
        /// <returns>The temperature of users current location</returns>
        public async Task<Weather> GetTemperatureOfCurrentUsersLocation(ClaimsPrincipal currentUser)
        {
            var httpClient = _httpClientFactory.CreateClient();

            Uri requestUri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q=Dresden,de&units=metric&APPID={_openWeatherOptions.ApiKey}");

            var stringResult = await httpClient.GetStringAsync(requestUri);
            dynamic weatherJson = JsonConvert.DeserializeObject<dynamic>(stringResult);

            var weather = new Weather();
            weather.Temperature = weatherJson.main.temp;
            weather.WeatherIcon = weatherJson.weather[0].icon;


            return weather;
        }
    }
}
