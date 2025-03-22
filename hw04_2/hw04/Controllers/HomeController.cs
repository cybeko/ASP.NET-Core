    using UniversityMVC.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Create()
        {
            ViewBag.Groups = await _context.Groups.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewBag.Groups = await _context.Groups.ToListAsync();
                return View(student);
            }
            _context.Add(student);
            await _context.SaveChangesAsync();
            Console.WriteLine("Added studnet");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DisplayStudents));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
