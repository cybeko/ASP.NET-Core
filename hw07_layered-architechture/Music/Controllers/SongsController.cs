using Microsoft.AspNetCore.Mvc;
using Music.BLL.DTO;
using Music.BLL.Interfaces;

namespace Music.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SongDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _songService.CreateSong(dto);
                return RedirectToAction("Index", "Home");
            }

            return View("Index", dto);
        }
    }
}