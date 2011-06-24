using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landscape.Core;

namespace Chinook.Web.Models.Album.ViewModels
{
    public class Index : JsonSerializable 
    {
        public IEnumerable<string> AlbumNames { get; set; }

        public Index(IEnumerable<string> albumNames)
        {
            AlbumNames = albumNames;
        }
    }
}