using System.Web;
using Newtonsoft.Json;

namespace Landscape.Core
{
    public abstract class JsonSerializable : IJsonSerializable
    {
        public HtmlString ToJson()
        {
            var data = JsonConvert.SerializeObject(this);
            return new HtmlString(data);
        }
    }
}
