using FiorelloDataFromDb.DAL;
using FiorelloDataFromDb.Extensions;
using FiorelloDataFromDb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Areas.FiorelloManager.Controllers
{
    [Area("FiorelloManager")]
    public class FlowerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FlowerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Flower> flowers = _context.Flowers.Include(f => f.FlowerImages).ToList();
            return View(flowers);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Flower flower)
        {
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid)
                return View();
            flower.FlowerCategories = new List<FlowerCategory>();
            flower.FlowerImages = new List<FlowerImage>();
            foreach (int id in flower.CateogryIds)
            {
                FlowerCategory fcategory = new FlowerCategory
                {
                    Flower = flower,
                    CategoryId = id
                };
                flower.FlowerCategories.Add(fcategory);
            }
            if (flower.ImageFiles.Count > 5)
            {
                ModelState.AddModelError("ImageFile", "You can choose max 5 images");
            }
            foreach (var image in flower.ImageFiles)
            {
                if (!image.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "You can select only image files");
                    return View();
                }
                if (!image.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFiles", "Image size max can be 2 mb");
                    return View();
                }
            }
            foreach (var image in flower.ImageFiles)
            {
                FlowerImage flowerImage = new FlowerImage
                {
                    Image = image.SaveImg(_env.WebRootPath, "assets/images"),
                    isMain = flower.FlowerImages.Count < 1 ? true : false,
                    Flower = flower
                };
                flower.FlowerImages.Add(flowerImage);
            }
            _context.Flowers.Add(flower);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            Flower flower = _context.Flowers.Include(f => f.FlowerCategories).Include(f => f.FlowerImages).FirstOrDefault(f => f.Id == id);
            if (flower == null)
                return NotFound();
            return View(flower);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Flower flower)
        {
            ViewBag.Categories = _context.Categories.ToList();
            Flower existedFlower = _context.Flowers.Include(f => f.FlowerImages).Include(f => f.FlowerCategories).FirstOrDefault(f => f.Id == flower.Id);
            if (!ModelState.IsValid)
                return View();
            if (existedFlower == null) return NotFound();
            if (flower.ImageFiles != null)
            {
                foreach (var image in flower.ImageFiles)
                {
                    if (!image.IsImage())
                    {
                        ModelState.AddModelError("ImageFiles", "Please select image file only");
                        return View(existedFlower);
                    }
                    if (!image.CheckSize(2))
                    {
                        ModelState.AddModelError("ImageFiles", "Image size max can be 2 mb");
                        return View(existedFlower);
                    }

                }
                List<FlowerImage> removableImages = existedFlower.FlowerImages.Where(fi => !flower.ImageIds.Contains(fi.Id)).ToList();
                existedFlower.FlowerImages.RemoveAll(fi => removableImages.Any(ri => ri.Id == fi.Id));
                foreach (var item in removableImages)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", item.Image);
                }
                foreach (var image in flower.ImageFiles)
                {
                    FlowerImage flowerImage = new FlowerImage
                    {
                        Image = image.SaveImg(_env.WebRootPath, "assets/images"),
                        isMain = false,
                        FlowerId = existedFlower.Id
                    };
                    existedFlower.FlowerImages.Add(flowerImage);
                }
                //List<FlowerCategory> removableCategories = existedFlower.FlowerCategories.Where(fc => !flower.CateogryIds.Contains(fc.Id)).ToList();
                //existedFlower.FlowerCategories.RemoveAll(fc => removableCategories.Any(rc => fc.Id == rc.Id));
                //foreach (var categoryId in flower.CateogryIds)
                //{
                //    FlowerCategory flowerCategory = existedFlower.FlowerCategories.FirstOrDefault(fc => fc.CategoryId == categoryId);
                //    if (flowerCategory == null)
                //    {
                //        FlowerCategory fcategory = new FlowerCategory
                //        {
                //            CategoryId = categoryId,
                //            FlowerId = existedFlower.Id
                //        };
                //        existedFlower.FlowerCategories.Add(fcategory);
                //    }
                //}
            }
            List<FlowerCategory> removableCategories = existedFlower.FlowerCategories.Where(fc => !flower.CateogryIds.Contains(fc.Id)).ToList();
            existedFlower.FlowerCategories.RemoveAll(fc => removableCategories.Any(rc => fc.Id == rc.Id));
            foreach (var categoryId in flower.CateogryIds)
            {
                FlowerCategory flowerCategory = existedFlower.FlowerCategories.FirstOrDefault(fc => fc.CategoryId == categoryId);
                if (flowerCategory == null)
                {
                    FlowerCategory fcategory = new FlowerCategory
                    {
                        CategoryId = categoryId,
                        FlowerId = existedFlower.Id
                    };
                    existedFlower.FlowerCategories.Add(fcategory);
                }
            }

            existedFlower.Name = flower.Name;
            existedFlower.Price = flower.Price;
            existedFlower.Description = flower.Description;
            existedFlower.Dimension = flower.Dimension;
            existedFlower.SKUCode = flower.SKUCode;
            existedFlower.InStock = flower.InStock;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
