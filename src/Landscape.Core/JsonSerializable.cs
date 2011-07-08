using System.Web;
using Newtonsoft.Json;

namespace Landscape.Core
{
    public abstract class JsonSerializable : IJsonSerializable
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
