using System.Diagnostics;
using System.Web.Mvc;
using Example.ReadModel;

namespace Example.UI.Controllers
{
    public class MenuController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            var model = new MenuList().GetAllItems(1, 10);
            return JsonOrView(model);
        }

    }
}
