using Microsoft.AspNetCore.Mvc;
using hw05_guestbook.Models;
using System.Security.Cryptography;
using System.Text;
using hw05_guestbook.Repository;

namespace hw05_guestbook.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository _repository;

        public AccountController(IRepository repository)
        {
            _repository = repository;
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
                _repository.Create(user);
                _repository.Save();
                return RedirectToAction("Login");
            }
            return View(reg);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel log)
        {
            if (!ModelState.IsValid)
            {
                return View(log);
            }

            var users = await _repository.GetUserList();
            var user = users.FirstOrDefault(u => u.Login == log.Login);

            if (user == null)
            {
                ModelState.AddModelError("", "Wrong login or password");
                return View(log);
            }

            byte[] passwordBytes = Encoding.Unicode.GetBytes(user.Salt + log.Password);
            using var md5 = MD5.Create();
            byte[] byteHash = md5.ComputeHash(passwordBytes);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            foreach (var b in byteHash)
            {
                hash.Append(b.ToString("X2"));
            }

            if (user.Password != hash.ToString())
            {
                ModelState.AddModelError("", "Wrong login or password");
                return View(log);
            }

            CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(10) };
            Response.Cookies.Append("login", user.Login, option);

            return RedirectToAction("Index", "Home");
        }

    }
}
