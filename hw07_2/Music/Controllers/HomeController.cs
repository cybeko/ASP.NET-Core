using Microsoft.AspNetCore.Mvc;
using Music.BLL.DTO;
using Music.BLL.Interfaces;
using System.Diagnostics;

namespace Music.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService _songService;

        public HomeController(ILogger<HomeController> logger, ISongService songService)
        {
            _logger = logger;
            _songService = songService;
        }
        public async Task<IActionResult> Index()
        {
            var songs = await _songService.GetSongs();
            ViewBag.Songs = songs;
            return View(new SongDTO());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
