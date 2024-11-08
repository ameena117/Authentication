using AuthTask.Data;
using AuthTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthTask.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if(user.UserName != null && user.Email != null)
            {
                context.users.Add(user);
                user.CreatedAt = DateTime.Now;
                context.SaveChanges();
                return RedirectToAction(nameof(Login));
            }  
            
            ViewBag.Error = "All Fields Are Required";
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var CheckUser = context.users.Where(
                u => u.UserName == user.UserName 
                && u.Password == user.Password 
                && user.UserName != null 
                && user.Password != null
            );
            if (CheckUser.Any())
            {
                user.IsActive = true;
                context.SaveChanges();
                return View("Index",user);
            }
            ViewBag.Error = "Invalid UserName or Password";
            return View(user);
        }

        public IActionResult GetUsers() {
            var AllUsers = context.users.ToList();
            Console.WriteLine(AllUsers[0].Email);
            return View(AllUsers);
        }

        public IActionResult GetActiveUsers()
        {
            var ActiveUsers = context.users.ToList().Where(u=>u.IsActive);
            if (ActiveUsers != null)
            {
                return View("GetUsers", ActiveUsers);
            }
            return RedirectToAction(nameof(GetUsers));
        }

        public IActionResult GetNotActiveUsers()
        {
            var NotActiveUsers = context.users.ToList().Where(u => !u.IsActive);
            if (NotActiveUsers != null)
            {
                return View("GetUsers", NotActiveUsers);
            }
            return RedirectToAction(nameof(GetUsers));
        }

        public IActionResult ChangeStatus(Guid id)
        {
            var UserSelected = context.users.Find(id);
            if (UserSelected != null)
            {
                UserSelected.IsActive = !UserSelected.IsActive;
                context.SaveChanges();
                return RedirectToAction(nameof(GetUsers));
            }
            return NotFound();
        }

    }
}
