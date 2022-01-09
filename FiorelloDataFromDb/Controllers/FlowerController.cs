using FiorelloDataFromDb.DAL;
using FiorelloDataFromDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Controllers
{
    public class FlowerController : Controller
    {
        private readonly AppDbContext _context;
        public FlowerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Details(int id)
        {
            Flower flower = _context.Flowers.Include(f=>f.FlowerCategories).ThenInclude(fc=>fc.Category)
                .Include(f=>f.FlowerImages).Include(f=>f.FlowerTags).ThenInclude(ft=>ft.Tag).FirstOrDefault(f=>f.Id==id);
            return View(flower);
        }
    }
}
