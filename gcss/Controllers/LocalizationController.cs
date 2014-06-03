using System.Web;
using System.Web.Mvc;

namespace gcss.Controllers
{
    public class LocalizationController : Controller
    {

			public ActionResult ChangeCulture(string lang)
			{
				var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
				Response.AppendCookie(langCookie);
				return RedirectToAction("Index", "Home", new { culture = lang });
			}

	}
}