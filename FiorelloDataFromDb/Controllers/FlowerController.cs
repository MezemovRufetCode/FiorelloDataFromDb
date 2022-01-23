using FiorelloDataFromDb.DAL;
using FiorelloDataFromDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public FlowerController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Flowers.Count() / 2);
            List<Flower> model = _context.Flowers.Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category).Include(f => f.FlowerImages).Skip((page - 1) * 2).Take(2).ToList();
            return View(model);
        }
        public IActionResult Details(int id, int categoryId)
        {
            Flower flower = _context.Flowers.Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category)
                .Include(f => f.FlowerImages).Include(f=>f.Comments).ThenInclude(c=>c.AppUser).Include(f => f.FlowerTags).ThenInclude(ft => ft.Tag).FirstOrDefault(f => f.Id == id);
            if (flower == null)
            {
                return NotFound();
            }
            ViewBag.RelatedFlowers = _context.Flowers.Include(f => f.FlowerImages).Include(f => f.FlowerCategories).Where(f => f.FlowerCategories.FirstOrDefault().CategoryId == categoryId && f.Id != id).Take(4).ToList();
            //Bir nece kateqoriya uchun where ile yoxla

            return View(flower);
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Flower", new { id = comment.FlowerId });
            if (!_context.Flowers.Any(f => f.Id == comment.FlowerId))
                return NotFound();
            Comment cmnt = new Comment
            {
               Text=comment.Text,
               FlowerId=comment.FlowerId,
               WriteTime=DateTime.Now,
               AppUserId=user.Id
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Flower",new {id=comment.FlowerId});
        }

        [Authorize]
        
        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (!ModelState.IsValid) return RedirectToAction("Details", "Flower");
            if (!_context.Comments.Any(c => c.Id == id && c.IsAccess==true && c.AppUserId == user.Id))
                return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId==user.Id);
            
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Flower", new { id = comment.FlowerId });
        }
        //public IActionResult SearchResult(string Name, int? CategoryId)
        //{
        //    List<Flower> flowers = new List<Flower>();
        //    if (CategoryId != null)
        //    {
        //        flowers = _context.Flowers.Where(f => f.Name.ToLower().Contains(Name.ToLower()) && f.FlowerCategories.FirstOrDefault().CategoryId == CategoryId).Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category).Include(f => f.FlowerImages).ToList();
        //    }
        //    else
        //    {
        //        flowers = _context.Flowers.Where(f => f.Name.ToLower().Contains(Name.ToLower())).Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category).Include(f => f.FlowerImages).ToList();
        //    }
        //    ViewBag.SearchName = Name;
        //    ViewBag.Categories = _context.Categories.ToList();
        //    return View(flowers);
        //}
    }
}
