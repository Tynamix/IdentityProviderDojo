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
        public int MinCentigrade { get; }

        /// <summary>
        /// Maximum centigrade for this requirement
        /// </summary>
        public int MaxCentigrade { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="WeatherRequirememt"/>
        /// </summary>
        /// <param name="minCentigrade">Minimum centigrade for this requirement</param>
        /// <param name="maxCentigrade">Maximum centigrade for this requirement</param>
        public WeatherRequirememt(int minCentigrade = -60, int maxCentigrade = 60)
        {
            MinCentigrade = minCentigrade;
            MaxCentigrade = maxCentigrade;
        }
    }
}
