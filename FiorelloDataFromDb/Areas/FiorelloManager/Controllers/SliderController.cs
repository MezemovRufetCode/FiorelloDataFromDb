using FiorelloDataFromDb.DAL;
using FiorelloDataFromDb.Extensions;
using FiorelloDataFromDb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Areas.FiorelloManager.Controllers
{
    [Area("FiorelloManager")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private  IWebHostEnvironment _env;
        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> model = _context.Sliders.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
                return View();

            if (slider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You can input only image");
                return View();
            }
            //extensiondan evvelki variant
            //if (slider.ImageFile.Length / Math.Pow(2,20)>=2)
                if (slider.ImageFile.CheckSize(2))
                {
                ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                return View();
              }
            //if (!slider.ImageFile.ContentType.Contains("image/"))
            if (!slider.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Please choose image file");
                return View();
            }

            //extensiondan evvelki variant

            //string rootPath = @"C:\Users\RufetPC\source\repos\FiorelloDataFromDb\FiorelloDataFromDb\wwwroot\assets\images\";
            ////file name i tekrarlana biler,buna gore yaziriq Guidi
            //string filename = Guid.NewGuid().ToString() + slider.ImageFile.FileName;
            //string fullpath = Path.Combine(rootPath, filename);
            //using(FileStream fileStream=new FileStream(fullpath, FileMode.Create))
            //{
            //    slider.ImageFile.CopyTo(fileStream);
            //}
            //slider.Image = filename;
            slider.Image = slider.ImageFile.SaveImg(_env.WebRootPath,"assets/images");
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(s => s.Id == slider.Id);
            // bu yazilisin meqsedi sliderde qeyd elediyimiz required max lenght tipli seylerin duzgunluyunu yoxlayir
            if (existSlider == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return View(existSlider);
            if(slider.ImageFile!=null && slider.SignatureFile == null)
            {
                if (!slider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please select img file");
                    return View(existSlider);
                }
                if (!slider.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                    return View(existSlider);
                } 
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", existSlider.Image);
                existSlider.Image = slider.ImageFile.SaveImg(_env.WebRootPath, "assets/images");
            }

            else if(slider.ImageFile==null && slider.SignatureFile != null)
            {
                if (!slider.SignatureFile.IsImage())
                {
                    ModelState.AddModelError("SignatureFile", "Please select img file");
                    return View(existSlider);
                }
                if (!slider.SignatureFile.CheckSize(2))
                {
                    ModelState.AddModelError("SignatureFile", "Image size max can be 2 mb");
                    return View(existSlider);
                }
                if (existSlider.SignatureFile != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", existSlider.Signature);
                }
                existSlider.Signature = slider.SignatureFile.SaveImg(_env.WebRootPath, "assets/images");
            }
            else if(slider.ImageFile !=null && slider.SignatureFile != null)
            {
                if (!slider.SignatureFile.IsImage())
                {
                    ModelState.AddModelError("SignatureFile", "Please select img file");
                    return View(existSlider);
                }
                if (!slider.SignatureFile.CheckSize(2))
                {
                    ModelState.AddModelError("SignatureFile", "Image size max can be 2 mb");
                    return View(existSlider);
                }

                if (!slider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please select img file");
                    return View(existSlider);
                }
                if (!slider.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                    return View(existSlider);
                }
                if (existSlider.SignatureFile!=null){
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", existSlider.Signature);
                }
              
                existSlider.Signature = slider.SignatureFile.SaveImg(_env.WebRootPath, "assets/images");

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", existSlider.Image);
                existSlider.Image = slider.ImageFile.SaveImg(_env.WebRootPath, "assets/images");
            }
            existSlider.Title = slider.Title;
            existSlider.Desc = slider.Desc;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
