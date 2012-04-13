using System.Web.Mvc;

namespace DonateAnywhere.Web.Controllers
{
    public class ButtonTestsController : Controller
    {
        public ActionResult Index(string fileName)
        {
            var absoluteUrl = "http://" + Request.Url.Host;

            if(!Request.Url.IsDefaultPort)
            {
                absoluteUrl += ":" + Request.Url.Port;
            }

            absoluteUrl += Url.Content("~/");

            return View(fileName, new TestViewModel { SiteRoot = absoluteUrl });
        }

    }

    public class TestViewModel
    {
        public string SiteRoot { get; set; }
    }
}
