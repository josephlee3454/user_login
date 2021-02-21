 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using login.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
namespace login.Controllers
{
    public class HomeController : Controller
    {
     
        private MyContext _context { get; set; }

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            // List<User> AllUsers = _context.Users.ToList();

            return View();
        }

        [HttpGet("/login")]
        public IActionResult login()
        {
            // List<User> AllUsers = _context.Users.ToList();

            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User creUser)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(newUser => newUser.Email == creUser.Email))
                {
                    ModelState.AddModelError("Email", "email in use");
                    return View("Index", creUser);
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                creUser.Pass = Hasher.HashPassword(creUser, creUser.Pass);
                _context.Users.Add(creUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("LoggedInID", (int)creUser.UserId);
                return RedirectToAction("Sucess");
            }
            else
            {
                return View("Index", creUser);
            }
        }

        // [HttpGet("login")]
        // public IActionResult Login()
        // {
        //     return View();
        // }


        [HttpPost("login")]
        public IActionResult LoginUser(LogUser user)
        {
            if (ModelState.IsValid)
            {
                User userDb = _context.Users.FirstOrDefault(u => u.Email == user.logEm);
                if (userDb == null)
                {
                    ModelState.AddModelError("Email", "Email is invalid");
                    return View("Index");
                }

                 PasswordHasher<User> hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(userDb, userDb.Pass, user.logPass);
                if (result == 0)
                {
                    ModelState.AddModelError("Password", "Password invalid");
                    return View("Index");
                }
                 HttpContext.Session.SetInt32("LoggedInID", (int)userDb.UserId);

                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }
          [HttpGet("success")]
        public IActionResult Success()
        {

            var isInSession = HttpContext.Session.GetInt32("LoggedInID");
            if (isInSession > 0)

            {
                var currentUser= _context.Users.FirstOrDefault(user=> user.UserId==isInSession);
                return View(currentUser);
            }
            else
            {
                return RedirectToAction("Success");
            }
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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


    