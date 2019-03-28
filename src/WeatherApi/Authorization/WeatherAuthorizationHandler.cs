using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherApi.Business.Contracts;
using WeatherApi.Configuration;

namespace WeatherApi.Authorization
{
    public class WeatherAuthorizationHandler : AuthorizationHandler<WeatherRequirememt>
    {
        private readonly IWeatherLogic _weatherLogic;

        public WeatherAuthorizationHandler(IWeatherLogic weatherLogic)
        {
            _weatherLogic = weatherLogic;
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WeatherRequirememt requirement)
        {
            var temperature =  await _weatherLogic.GetTemperatureOfCurrentUsersLocation(context.User);

            if (temperature >= requirement.MinCentigrade
                && temperature <= requirement.MaxCentigrade)
            {
                context.Succeed(requirement);
            }
        }
    }
}