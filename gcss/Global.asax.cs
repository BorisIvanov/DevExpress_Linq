using System.Globalization;
using System.Threading;
using DevExpress.Web.Mvc;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web;



namespace gcss
{
	public class MvcApplication : System.Web.HttpApplication
  {
		
		protected void Application_Start()
    {
			AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
			
			/* DevExpress settings */
			ModelBinders.Binders.DefaultBinder = new DevExpressEditorsBinder();
			DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
    }

		protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
		{
			DevExpressHelper.Theme = "Office2010Blue";
		}

		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
			CultureInfo cultureInfo = new CultureInfo("ru");
			var handler = Context.Handler as MvcHandler;
			var routeData = handler != null ? handler.RequestContext.RouteData : null;
			if (routeData != null)
			{
				if (routeData.Values["culture"] != null)
				{
					var routeCulture = routeData != null ? routeData.Values["culture"].ToString() : null;
					var languageCookie = HttpContext.Current.Request.Cookies["lang"];
					var userLanguages = HttpContext.Current.Request.UserLanguages;
					// Set the Culture based on a route, a cookie or the browser settings,
					// or default value if something went wrong
					cultureInfo = new CultureInfo(
						routeCulture ?? (languageCookie != null ? languageCookie.Value : userLanguages != null ? userLanguages[0] : "ru")
						);
				}
			}
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
		}

  }

}
