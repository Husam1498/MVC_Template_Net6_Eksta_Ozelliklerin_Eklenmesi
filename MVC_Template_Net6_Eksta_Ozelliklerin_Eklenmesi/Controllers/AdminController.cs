using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Controllers
{
   // [Authorize(Roles = "admin,yetkili")]//rolu admin manager gibi olanlar erişebilir
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
