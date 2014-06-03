using System.Web.Mvc;

namespace gcss.Controllers
{
    public class HomeController : LocalizationController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}