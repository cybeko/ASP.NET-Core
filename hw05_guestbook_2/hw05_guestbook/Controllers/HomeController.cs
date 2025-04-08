using hw05_guestbook.Models;
using hw05_guestbook.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace hw05_guestbook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GuestbookContext _context;
        private readonly IRepository _repository;
        public HomeController(ILogger<HomeController> logger, GuestbookContext context, IRepository repository)
        {
            _logger = logger;
            _context = context;
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetMessages()
        {
            var messages = _context.Messages
                .Include(m => m.User) 
                .Select(m => new
                {
                    m.Id,
                    m.Text,
                    m.DateTime,
                    User = new { m.User.Login }
                })
                .ToList();

            return Json(messages);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAjax([FromBody] RegisterModel reg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid credentials" });
            }

            var user = new User();
            user.Login = reg.Login;

            byte[] saltbuf = new byte[16];
            using (var rand = RandomNumberGenerator.Create())
                rand.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append($"{saltbuf[i]:X2}");
            string salt = sb.ToString();

            byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);
            using var md5 = MD5.Create();
            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            foreach (var b in byteHash)
                hash.Append($"{b:X2}");

            user.Password = hash.ToString();
            user.Salt = salt;

            await _repository.Create(user);
            await _repository.Save();

            return Json(new { success = true, message = "Registered successfully" });
        }
        [HttpPost]
        public async Task<IActionResult> LoginAjax([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid credentials" });
            }

            var user = await _repository.GetUserByLogin(model.Login);
            if (user == null)
            {
                return BadRequest(new { success = false, message = "User not found" });
            }

            byte[] passwordBytes = Encoding.Unicode.GetBytes(user.Salt + model.Password);
            using var md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(passwordBytes);

            StringBuilder sb = new StringBuilder();
            foreach (var b in hashBytes)
                sb.Append($"{b:X2}");
            string hash = sb.ToString();

            if (hash != user.Password)
            {
                return BadRequest(new { success = false, message = "wrong password" });
            }

            return Json(new { success = true, message = "Login successful" });
        }

        public IActionResult Index()
        {
            if (Request.Cookies["login"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var messages = _context.Messages.Include(m => m.User).ToList();
            return View(messages);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
