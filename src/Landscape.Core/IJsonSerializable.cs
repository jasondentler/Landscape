using System.Web;

namespace Landscape.Core
{
    public interface IJsonSerializable
    {

        HtmlString ToJson();

    }
}
