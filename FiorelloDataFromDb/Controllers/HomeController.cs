using FiorelloDataFromDb.DAL;
using FiorelloDataFromDb.Models;
using FiorelloDataFromDb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeWM homeWM = new HomeWM()
            {
                Sliders = _context.Sliders.ToList(),
                Experts=_context.Experts.ToList(),
                Reviews=_context.Reviews.ToList(),
                Categories=_context.Categories.ToList(),
                Flowers=_context.Flowers.Include(f=>f.FlowerImages).Include(f=>f.FlowerCategories).ThenInclude(fc=>fc.Category).ToList(),
                Setting=_context.Settings.FirstOrDefault()

            };
            return View(homeWM);
        }
    }
}
