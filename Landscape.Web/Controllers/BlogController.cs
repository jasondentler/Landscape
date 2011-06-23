using System.Web.Mvc;
using Landscape.Dtos.Blog.ViewModels;

namespace Landscape.Web.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View(new Index
                            {
                                Name = "Jason"
                            });
        }

    }
}
