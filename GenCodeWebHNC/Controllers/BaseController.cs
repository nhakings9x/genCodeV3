using GenCodeWebHNC.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using System.Text;

namespace GenCodeWebHNC.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger _logger;

        public BaseController()
        {
        }

        protected virtual string RenderHtml(ViewResult result)
        {
            IRoutingFeature routingFeature = base.HttpContext.Features.Get<IRoutingFeature>();
            RouteData routeData = routingFeature.RouteData;
            string viewName = result.ViewName ?? (routeData.Values["action"] as string);
            ActionContext actionContext = new ActionContext(base.HttpContext, routeData, new ControllerActionDescriptor());
            IOptions<MvcViewOptions> requiredService = base.HttpContext.RequestServices.GetRequiredService<IOptions<MvcViewOptions>>();
            HtmlHelperOptions htmlHelperOptions = requiredService.Value.HtmlHelperOptions;
            ViewEngineResult viewEngineResult = result.ViewEngine?.FindView(actionContext, viewName, isMainPage: true) ?? requiredService.Value.ViewEngines.Select((IViewEngine x) => x.FindView(actionContext, viewName, isMainPage: true)).FirstOrDefault((ViewEngineResult x) => x != null);
            IView view = viewEngineResult.View;
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter writer = new StringWriter(stringBuilder))
            {
                ViewContext context = new ViewContext(actionContext, view, result.ViewData, result.TempData, writer, htmlHelperOptions);
                view.RenderAsync(context).GetAwaiter().GetResult();
            }

            return stringBuilder.ToString();
        }

        protected virtual Result<string> RenderPartialHtml<TModel>(string viewNamePath, TModel model)
        {
            string html;
            Result result = RenderPartialHtml(viewNamePath, model, out html);
            if (result.IsError())
            {
                return Result.Error<string>($"Lỗi render: {result.ToString()}");
            }

            return Result.Ok(html);
        }

        protected virtual Result RenderPartialHtml<TModel>(string viewNamePath, TModel model, out string html)
        {
            if (string.IsNullOrEmpty(viewNamePath))
            {
                viewNamePath = base.ControllerContext.ActionDescriptor.ActionName;
            }

            base.ViewData.Model = model;
            html = "";
            using StringWriter stringWriter = new StringWriter();
            try
            {
                IViewEngine viewEngine = base.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewEngineResult = null;
                viewEngineResult = ((!viewNamePath.EndsWith(".cshtml")) ? viewEngine.FindView(base.ControllerContext, viewNamePath, isMainPage: false) : viewEngine.GetView(viewNamePath, viewNamePath, isMainPage: false));
                if (!viewEngineResult.Success)
                {
                    List<string> list = ((viewEngineResult.SearchedLocations != null) ? viewEngineResult.SearchedLocations.ToList() : new List<string>());
                    return Result.Error("View notfound!!" + Environment.NewLine + string.Join(Environment.NewLine, list));
                }

                ViewContext context = new ViewContext(base.ControllerContext, viewEngineResult.View, base.ViewData, base.TempData, stringWriter, new HtmlHelperOptions());
                viewEngineResult.View.RenderAsync(context).GetAwaiter().GetResult();
                html = stringWriter.GetStringBuilder().ToString();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error("[RenderPartialHtml]: Failed - " + ex.Message);
            }
        }
    }
}
