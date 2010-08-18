using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GG.DonateAnywhere.Controllers
{
    public class LandingPageController : Controller
    {
        //
        // GET: /LandingPager/

        public ActionResult Index()
        {
            return View();
        }

    }
}
