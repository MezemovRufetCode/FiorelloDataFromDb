using FiorelloDataFromDb.DAL;
using FiorelloDataFromDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Areas.FiorelloManager.Controllers
{
    [Area("FiorelloManager")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Tag> model = _context.Tags.Include(t => t.FlowerTags).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
                //return Content("Max length can be 20");
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            return View(tag);
        }
        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Tag exTag = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (exTag == null)
            {
                return NotFound();
            }
            Category sname = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == tag.Name.ToLower());
            if (sname != null)
            {
                ModelState.AddModelError("", "This name existed,try different");
                return View();
            }
            exTag.Name = tag.Name;
            //_context.Tags.Remove(exTag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);

            if (tag == null)
                return Json(new { status = 404 });
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
