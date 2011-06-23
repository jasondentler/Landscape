using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Landscape.Core
{

    public static class HtmlHelperExtensions
    {

        public static void RegisterTemplate(this HtmlHelper html, string id, string appRelativeUrl)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);

            if (!appRelativeUrl.StartsWith("~/"))
                appRelativeUrl = "~/" + appRelativeUrl;

            var url = urlHelper.Content(appRelativeUrl);

            var registeredTemplates = GetRegisteredTemplates(html);
            registeredTemplates[id] = url;
        }

        public static MvcHtmlString RenderTemplates(this HtmlHelper html)
        {
            var registeredTemplates = GetRegisteredTemplates(html);
            var output = new StringBuilder();
            foreach (var template in registeredTemplates)
            {
                output.AppendFormat("<script id='{0}' src='{1}' type='text/html'></script>",
                                    template.Key, template.Value);
                output.AppendLine();
            }
            return new MvcHtmlString(output.ToString());
        }

        private static IDictionary<string, string> GetRegisteredTemplates(HtmlHelper helper)
        {
            return GetRegisteredTemplates(helper.ViewData);
        }

        private static IDictionary<string, string> GetRegisteredTemplates(ViewDataDictionary viewData)
        {
            const string key = "RegisteredTemplates";
            if (!viewData.ContainsKey(key))
            {
                var dictionary = new Dictionary<string, string>();
                viewData[key] = dictionary;
                return dictionary;
            }
            return (IDictionary<string, string>)viewData[key];
        }



    }
}
