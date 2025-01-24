using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using UserRegistation_MVC.Entities;

namespace UserRegistation_MVC.Controllers
{
    public class AddUserController : Controller
    {
        private string path = @"Helpers\UsersData.json";
        private readonly List<User> _users;

        public AddUserController()
        {
            _users = GetAllUser();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            if(user is null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = _users.Max(u => u.Id) + 1;

                string urlPattern = @"^https?:\/\/.*\.(png|jpg|jpeg|gif)$";
                if (!Regex.IsMatch(user.ImagePath, urlPattern, RegexOptions.IgnoreCase))
                { 
                    user.ImagePath = "https://www.webiconio.com/_upload/255/image_255.svg"; 
                }

                _users.Add(user);

                SaveUsers();

                return RedirectToAction("Index", "Home");
            }

            return View(user); 
        }

        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user is null)
            {
                return NotFound();
            }

            _users.Remove(user);

            SaveUsers();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateUser(User updateUser)
        {
            var user = _users.FirstOrDefault(u => u.Id == updateUser.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = updateUser.Name;
            user.Surname = updateUser.Surname;
            user.BirthDate = updateUser.BirthDate;
            user.Number = updateUser.Number;
            user.ImagePath = updateUser.ImagePath;



            SaveUsers();

            return RedirectToAction("Index", "Home");
        }


        public List<User> GetAllUser()  
        {
            if (!System.IO.File.Exists(path))
            {
                return new List<User>();
            }

            var json = System.IO.File.ReadAllText(path);
            var users = JsonSerializer.Deserialize<List<User>>(json);
            return users ?? new List<User>(); 
        }

        public void SaveUsers()
        {
            var updatedJson = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(path, updatedJson);
        }
    }
}
