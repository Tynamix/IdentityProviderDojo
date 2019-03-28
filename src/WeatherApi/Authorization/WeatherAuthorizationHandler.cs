using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherApi.Configuration;

namespace WeatherApi.Authorization
{
    public class WeatherAuthorizationHandler : AuthorizationHandler<WeatherRequirememt>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<OpenWeatherApiOptions> _settings;

        public WeatherAuthorizationHandler(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherApiOptions> settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings;
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WeatherRequirememt requirement)
        {
            var httpClient = _httpClientFactory.CreateClient();

            Uri requestUri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q=Dresden,de&units=metric&APPID={_settings.Value.ApiKey}");

            var stringResult = await httpClient.GetStringAsync(requestUri);
            dynamic weatherJson = JsonConvert.DeserializeObject<dynamic>(stringResult);

            double temperature = weatherJson.main.temp;

            if (temperature >= requirement.MinCentigrade
                && temperature <= requirement.MaxCentigrade)
            {
                context.Succeed(requirement);
            }
        }
    }
}