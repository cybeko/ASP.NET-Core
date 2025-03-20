    using UniversityMVC.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

    namespace UniversityMVC.Controllers
    {
        public class HomeController : Controller
        {
        private readonly ILogger<HomeController> _logger;
        private readonly UniversityContext _context;

        public HomeController(ILogger<HomeController> logger, UniversityContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> DisplayStudents()
        {
            var students = await _context.Students.Include(s => s.Group).ToListAsync();
            return View(students);
        }
        public async Task<IActionResult> DisplaySubjects()
        {
            var subjects = await _context.Subjects.ToListAsync();
            return View(subjects);
        }
        public async Task<IActionResult> DisplayTeachers()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }
        public async Task<IActionResult> DisplaySubjectGroups()
        {
            var subjectGroups = await _context.SubjectsGroups.Include(sg => sg.Subject).Include(sg => sg.Group).Include(sg => sg.Teacher).ToListAsync();
            return View(subjectGroups);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
