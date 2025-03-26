using Microsoft.AspNetCore.Mvc;
using hw05_guestbook.Models;
using System.Security.Cryptography;
using System.Text;

namespace hw05_guestbook.Controllers
{
    public class AccountController : Controller
    {
        private readonly GuestbookContext _context;
        public AccountController(GuestbookContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Login = reg.Login;
                byte[] saltbuf = new byte[16];

                RandomNumberGenerator rand = RandomNumberGenerator.Create();
                rand.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                var md5 = MD5.Create();

                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));
                user.Password = hash.ToString();
                user.Salt = salt;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(reg);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel log)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                var users = _context.Users.Where(a => a.Login == log.Login);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                var user = users.First();
                string? salt = user.Salt;

                byte[] password = Encoding.Unicode.GetBytes(salt + log.Password);

                var md5 = MD5.Create();

                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                {
                    hash.Append(string.Format("{0:X2}", byteHash[i]));
                }
                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Append("login", user.Login, option);

                return RedirectToAction("Index", "Home");
            }
            Console.WriteLine("Model state is invalid");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(log);
        }
    }
}
