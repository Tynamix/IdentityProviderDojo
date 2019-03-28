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
    public class WeatherController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
