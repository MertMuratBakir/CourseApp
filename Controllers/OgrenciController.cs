using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Controllers 
{
    public class OgrenciController: Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}