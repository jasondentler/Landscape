using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Landscape.Core;
using Restbucks.UI.Models.Menu.ViewModels;

namespace Restbucks.UI.Controllers
{
    public class MenuController : LandscapeController 
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //[NonAction]
        //public Index GetIndex()
        //{
        //    var req = System.Net.WebRequest.Create("http://restbuckson.net/menu");
        //    var resp = (HttpWebResponse) req.GetResponse();
        //    XDocument document;

        //    using (var respstrm = resp.GetResponseStream())
        //    {
        //        document = XDocument.Load(respstrm);
        //    }

        //    var items = document.Root
        //        .Elements("item")
        //        .ToDictionary(
        //            item => item.Element("Name").Value,
        //            item => decimal.Parse(item.Element("Price").Value)
        //        );

        //    return new Index(items);
        //}

    }
}
