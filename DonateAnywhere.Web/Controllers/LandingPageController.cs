using System.Web.Mvc;
using DonateAnywhere.Web.Code;
using GG.DonateAnywhere.Core;

namespace DonateAnywhere.Web.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly IDonateAnywhereRequestContextFactory _contextFactory;
        private readonly IDonateAnywhereService _donateAnywhereService;

        public LandingPageController(IDonateAnywhereRequestContextFactory contextFactory, IDonateAnywhereService donateAnywhereService)
        {
            _contextFactory = contextFactory;
            _donateAnywhereService = donateAnywhereService;
        }

        public ActionResult Index()
        {
            var donateAnywhereContext = _contextFactory.BuildContext(HttpContext);
            if (!donateAnywhereContext.EnoughInformationToBuildSuggestions)
            {
                return RedirectToAction("NotEnoughInformation");
            }

            var donateAnywhereResult = _donateAnywhereService.EvaluateRequest(donateAnywhereContext);

            if (donateAnywhereResult.Results.Count == 0)
            {
                return RedirectToAction("NoResultsFound");
            }

            return View(donateAnywhereResult);
        }

        public ActionResult NotEnoughInformation()
        {
            return View();
        }

        public ActionResult NoResultsFound()
        {
            return View();
        }

    }
}
