using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.Areas.FiorelloManager.Controllers
{
    [Area("FiorelloManager")]
    //bu onun ucundur ki her hansi bir actiona muraciet elemek ucun auhorize olmaq lazimdi
    //mes burda ona gore yazdiq ki adi istifadeci dashboard sehifesini aca bilmesin
    //bunu ayrica actiona da vermek olur.
    //Rollari da qeyd edirik ki kimler aca biler 
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
