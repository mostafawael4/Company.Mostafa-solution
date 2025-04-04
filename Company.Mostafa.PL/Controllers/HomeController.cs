using Company.Mostafa.PL.ViewModels;
using Company.Mostafa.PL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Company.Mostafa.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scope01;
        private readonly IScopedService scope02;
        private readonly ITransientService transient01;
        private readonly ITransientService transient02;
        private readonly ISingletonService singleton01;
        private readonly ISingletonService singleton02;

        public HomeController(
            ILogger<HomeController> logger,
            IScopedService scope01,
            IScopedService scope02,
            ITransientService transient01,
            ITransientService transient02,
            ISingletonService singleton01,
            ISingletonService singleton02
            )
        {
            _logger = logger;
            this.scope01 = scope01;
            this.scope02 = scope02;
            this.transient01 = transient01;
            this.transient02 = transient02;
            this.singleton01 = singleton01;
            this.singleton02 = singleton02;
        }

        public string Test()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"scope01:{ scope01.GetGuid()}\n");
            sb.Append($"scope02:{scope02.GetGuid()}\n\n");
            sb.Append($"transient01:{transient01.GetGuid()}\n");
            sb.Append($"transient02:{transient02.GetGuid()}\n\n");
            sb.Append($"singleton01:{singleton01.GetGuid()}\n");
            sb.Append($"singleton02:{singleton02.GetGuid()}\n\n");
            return sb.ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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

