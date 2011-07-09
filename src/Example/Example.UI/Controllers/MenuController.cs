using System;
using System.Diagnostics;
using System.Web.Mvc;
using Example.ReadModel;

namespace Example.UI.Controllers
{
    public class MenuController : Controller
    {
        
        [HttpGet]
        public ActionResult Index(int? pageNumber)
        {
            if (!pageNumber.HasValue) pageNumber = 1;
            if (pageNumber <= 0) pageNumber = 1;
            var model = new MenuList().GetAllItems(pageNumber.Value, 5);
            return JsonOrView(model);
        }

    }
}
