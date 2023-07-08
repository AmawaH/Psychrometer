using Microsoft.AspNetCore.Mvc;
using PsychroASP.Models;
using System.Diagnostics;

namespace PsychroASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Calculate(Readings readings)
        {
            double alfa, pd, pw, dw, dd;
            alfa = 1e-6 * (593.1 + (135.1 / Math.Sqrt(readings.SpeedWind)) + (48 / readings.SpeedWind)); //Залежнiсть психрометричного коефiцiєнта вiд швидкостi руху повiтря на м/с, формула Зворикiна
            pd = 4.5845 * Math.Exp((readings.TempDry * (18.678 - (readings.TempDry / 234.5))) / (257.14 + readings.TempDry)); //парцiальний тиск водяної пари при температурi сухого термометра 
            pw = 4.5845 * Math.Exp((readings.TempWet * (18.678 - (readings.TempWet / 234.5))) / (257.14 + readings.TempWet)); //парцiальний тиск водяної пари при температурi вологого термометра
            dd = (288.97 * pd) / (273.15 + readings.TempDry); //густина насиченої водяної пари при температурi сухого термометра, г/м³
            dw = (288.97 * pw) / (273.15 + readings.TempWet); //густина насиченої водяної пари при температурi вологого термометра, г/м³
            readings.Humidity = 100 * ((dw / dd) - alfa * readings.Pressure * (readings.TempDry - readings.TempWet) / dd); //обчислення вiдносної вологостi повiтря
            return View(readings);
        }

        public IActionResult MoreInfo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}