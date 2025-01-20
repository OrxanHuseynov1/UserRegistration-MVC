using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using UserRegistation_MVC.Entities;

namespace UserRegistation_MVC.Controllers
{
    public class AddUserController : Controller
    {
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                var path = @"Helpers\UsersData.json";
                var json = System.IO.File.ReadAllText(path);
                var users = JsonSerializer.Deserialize<List<User>>(json);

                user.Id = users.Max(u => u.Id) + 1;
                users.Add(user);

                var updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(path, updatedJson);
                return RedirectToAction("Index","Home");
            }
            return View(user);
        }
    }
}
