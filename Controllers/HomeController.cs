using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using SignalR.Models;
using SignalR.Utilities;
using System.Diagnostics;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> deathlyHub;
        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowsHub> _deathlyHub)
        {
            _logger = logger;
            deathlyHub = _deathlyHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DealthyHallowRace.ContainsKey(type))
            {
                SD.DealthyHallowRace[type]++;
            }
            await deathlyHub.Clients.All.SendAsync("updateRace",
                SD.DealthyHallowRace[SD.Cloak],
                SD.DealthyHallowRace[SD.Stone],
                SD.DealthyHallowRace[SD.Wand]);
            return Accepted();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}