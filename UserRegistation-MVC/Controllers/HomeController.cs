using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using UserRegistation_MVC.Entities;
namespace UserRegistation_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var path = @"Helpers\UsersData.json";
            var json = System.IO.File.ReadAllText(path);
            var Users= JsonSerializer.Deserialize<List<User>>(json).ToList();
            return View(Users);
        } 


    }
}
