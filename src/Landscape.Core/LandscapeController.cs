using System.Web.Mvc;

namespace Landscape.Core
{
    public class LandscapeController : System.Web.Mvc.Controller
    {

        [NonAction]
        public virtual ActionResult JsonOrView<TModel>(TModel model)
            where TModel : IJsonSerializable 
        {
            if (Request.IsAjaxRequest())
                return Json(model);
            return View(model);
        }

        [NonAction]
        public virtual ActionResult JsonOrView(object model)
        {
            if (Request.IsAjaxRequest())
                return Json(model);
            return View(model);
        }

        [NonAction]
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

    }
}
