using hw06_dependency_injection.Models;
using hw06_dependency_injection.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hw06_dependency_injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDisplay _webDisplay;
        private readonly ISave _fileDisplay;
        private readonly List<Note> _notes;

        public HomeController(IDisplay webDisplay, ISave fileDisplay)
        {
            _webDisplay = webDisplay;
            _fileDisplay = fileDisplay;

            _notes = new List<Note>
            {
                new Note { Name = "Note1", Text = "First note text", Date = DateOnly.FromDateTime(DateTime.Now), Tags = new List<string> { "Tag1", "Tag2" } },
                new Note { Name = "Note2", Text = "Second note text", Date = DateOnly.FromDateTime(DateTime.Now), Tags = new List<string> { "Tag1" } }
            };
        }
        public IActionResult Index(string message = null)
        {
            ViewBag.Message = message;
            return View(null);
        }
        public IActionResult DisplayNotes()
        {
            return View("Index", _notes);
        }
        public IActionResult SaveToFile()
        {
            _fileDisplay.Save(_notes);
            return RedirectToAction("Index", new { message = "Notes saved to file" });
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
