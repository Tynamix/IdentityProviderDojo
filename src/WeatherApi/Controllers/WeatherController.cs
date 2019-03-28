using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using WeatherApi.Business.Contracts;
using WeatherApi.Configuration;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherLogic _weatherLogic;

        public WeatherController(IWeatherLogic weatherLogic)
        {
            _weatherLogic = weatherLogic;
        }

        [Authorize(Policy = "AtLeast10")]
        [HttpGet("warmWeather")]
        public async Task<ActionResult<Weather>> Warm()
        {
            return await _weatherLogic.GetTemperatureOfCurrentUsersLocation(this.User);
        }

        [Authorize(Policy = "AtMost10")]
        [HttpGet("coldWeather")]
        public async Task<ActionResult<Weather>> Cold()
        {
            return await _weatherLogic.GetTemperatureOfCurrentUsersLocation(this.User);
        }

    }
}
