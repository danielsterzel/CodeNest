using CodeNest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context) => _context = context;

    //Get /Account/RegisterUser
    [HttpGet]
    public IActionResult RegisterUser() => View();

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult RegisterUser(RegisterViewModel model) 
    {
        if (!ModelState.IsValid) return View(model);
        if (model.UserName.Contains("_teacher")) return View(model);
        var user = new User
        {
            UserName = model.UserName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Password = model.Password,
            Role = "user"
        };
        var hasher = new PasswordHasher<User>();
        user.Password = hasher.HashPassword(user, model.Password);
        _context.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Login");
    }

    // GET /Account/RegisterTeacher
    [HttpGet] public IActionResult RegisterTeacher() => View();

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult RegisterTeacher(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Message = "Doesnt work";
            return View(model);
        }

        var teacher = new Teacher()
        {
            UserName = model.UserName,
            Name = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Password = model.Password,
            Role = "Teacher"
        };
        var hasher = new PasswordHasher<Teacher>();
        teacher.Password = hasher.HashPassword(teacher, model.Password);
        _context.Teachers.Add(teacher);
        _context.SaveChanges();
        return RedirectToAction("Login");
    } 

    [HttpGet]
    public IActionResult Login() => View();

    [ValidateAntiForgeryToken]
    [HttpPost]
    //this method may require teachers to have unique logins and restricting User usernames to not contain a specific keyword. If 
    // a user and a teacher has the same username and the same password it will find it in the Users database and login a teacher to 
    // a user's account.
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (model.Role == "User")
        {
            // here we try to get the user that has this specific username or email. The FirstOrDefault will return null if we cannot
            // find a user with this username and or email.
            var user = _context.Users.FirstOrDefault(u =>
                (u.Email == model.Identifier || u.UserName == model.Identifier));
            if (user != null)
            {
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);
                if (result == PasswordVerificationResult.Success)
                {

                    HttpContext.Session.SetString("UserRole", "User");
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    return RedirectToAction("Dashboard", "User");
                }
            }
            ModelState.AddModelError("","Niepoprawne dane logowania.");
            return View(model);
        }
        else if (model.Role == "Teacher")
        {
            var teacher =
                _context.Teachers.FirstOrDefault(t => (t.UserName == model.Identifier || t.Email == model.Identifier));
            if (teacher != null)
            {
                var hasher = new PasswordHasher<Teacher>();
                var result = hasher.VerifyHashedPassword(teacher, teacher.Password, model.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("UserRole", "Teacher");
                    HttpContext.Session.SetString("UserId", teacher.TeacherId.ToString());
                    return RedirectToAction("Dashboard", "Teacher");
                }
            }
        }else if (model.Role == "Admin")
        {
            var admin = _context.Users.FirstOrDefault(a =>
                (a.Email == model.Identifier || a.UserName == model.Identifier) && a.Role == "Admin");
            if (admin != null)
            {
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(admin, admin.Password, model.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("UserRole", "Admin");
                    HttpContext.Session.SetString("UserId", admin.UserId.ToString());
                    return RedirectToAction("AdminDashboard", "Admin");
                }
            }
        }


        ModelState.AddModelError("", "Niepoprawne dane logowania.");
        return View(model);
        
    }


    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index","Home");
    }
}