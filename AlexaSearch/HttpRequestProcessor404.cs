using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;

namespace AlexaSearch
{
    public class HttpRequestProcessor404 : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (Sitecore.Context.Item != null || Sitecore.Context.Site == null || Sitecore.Context.Database == null
                || Sitecore.Context.Database.Name == "core"
                || args.RequestUrl.AbsoluteUri.ToLower().Contains("/sitecore/api/layout/render/jss")
                || args.RequestUrl.AbsoluteUri.ToLower().Contains("/sitecore/api/jss/import")
                || args.RequestUrl.AbsoluteUri.ToLower().Contains("/api/sitecore/"))
            {
                return;
            }
            var pageNotFound = Sitecore.Context.Database.GetItem(@"{AEC32B57-496D-4ACF-A49D-C08FACB6EFA8}");
            if (pageNotFound == null)
                return;
            args.ProcessorItem = pageNotFound;
            Sitecore.Context.Item = pageNotFound;
            args.HttpContext.Response.StatusCode = 404;
        }
    }
}