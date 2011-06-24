using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chinook.Web.Models.Album.ViewModels;
using Landscape.Core;

namespace Chinook.Web.Controllers
{
    public class AlbumController : LandscapeController 
    {
        //
        // GET: /Album/

        public ActionResult Index()
        {
            var albumNames = new[]
                                 {
                                     "For Those About To Rock We Salute You",
                                     "Balls to the Wall",
                                     "Restless and Wild",
                                     "Let There Be Rock",
                                     "Big Ones",
                                     "Jagged Little Pill",
                                     "Facelift",
                                     "Warner 25 Anos",
                                     "Plays Metallica By Four Cellos",
                                     "Audioslave"
                                 };

            return View(new Index(albumNames));
        }

    }
}
