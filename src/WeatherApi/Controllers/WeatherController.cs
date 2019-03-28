using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [Authorize(Policy = "AtLeast10")]
        [HttpGet("warmWeather")]
        public ActionResult<IEnumerable<string>> Warm()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize(Policy = "AtMost10")]
        [HttpGet("coldWeather")]
        public ActionResult<IEnumerable<string>> Cold()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
