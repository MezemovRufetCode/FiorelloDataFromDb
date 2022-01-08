using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Controllers
{
    public class Flower : Controller
    {
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
