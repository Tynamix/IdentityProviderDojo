using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WeatherApi.Authorization
{
    public class WeatherRequirememt : IAuthorizationRequirement
    {
        /// <summary>
        /// Minimum centigrade for this requirement
        /// </summary>
        public int MinCentigradeForGoodWeather { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="WeatherRequirememt"/>
        /// </summary>
        /// <param name="minCentigradeForGoodWeather">Minimum centigrade for this requirement</param>
        public WeatherRequirememt(int minCentigradeForGoodWeather)
        {
            MinCentigradeForGoodWeather = minCentigradeForGoodWeather;
        }
    }
}
