using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Business.Contracts
{
    public interface IWeatherLogic
    {
        /// <summary>
        /// Gets the temperature of current users location
        /// </summary>
        /// <param name="currentUser">The currently logged in user</param>
        /// <returns>The temperature of users current location</returns>
        Task<double> GetTemperatureOfCurrentUsersLocation(ClaimsPrincipal currentUser);
    }
}
