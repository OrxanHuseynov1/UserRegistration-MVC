using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using UserRegistation_MVC.Entities;
namespace UserRegistation_MVC.Controllers
{
    public class HomeController : Controller
    {
        private string path = @"Helpers\UsersData.json";
        public List<User> _users;

        public IActionResult Index()
        {
            GetAllUser();
            return View(_users);
        } 

        public IActionResult SortUsers(string sort)
        {
            GetAllUser();
            if(sort is "az") _users = _users.OrderBy(u => u.Name).ToList();
            else _users = _users.OrderByDescending(u => u.Name).ToList();

            return View("Index", _users);
        }

        public List<User> GetAllUser()
        {
            if (!System.IO.File.Exists(path)) return new List<User>();
            

            var json = System.IO.File.ReadAllText(path);
            var users = JsonSerializer.Deserialize<List<User>>(json);
            _users = users.ToList();
            return users ?? new List<User>();

        }


    }
}
