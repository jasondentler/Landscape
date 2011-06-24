using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Landscape.Core
{
    public class CustomJsonResult : JsonResult 
    {

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("GET not allowed.");

            var response = context.HttpContext.Response;

            response.ContentType = string.IsNullOrEmpty(ContentType)
                                       ? ContentType
                                       : "application/json";
            
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data == null)
                return;

            var serializableData = Data as IJsonSerializable;

            if (serializableData != null)
            {
                response.Write(serializableData.ToJson());
                return;
            }

            response.Write(JsonConvert.SerializeObject(Data));
        }

    }
}
