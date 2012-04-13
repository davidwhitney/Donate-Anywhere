using System.Web.Mvc;
using DonateAnywhere.Web.Code;
using GG.DonateAnywhere.Core;
using GG.DonateAnywhere.Core.Http;
using GG.DonateAnywhere.Core.PageAnalysis;
using GG.DonateAnywhere.Core.Sanitise;
using GG.DonateAnywhere.Core.Searching;

namespace DonateAnywhere.Web.Controllers
{
    public class ButtonController : Controller
    {
        private readonly IDonateAnywhereRequestContextFactory _contextFactory;
        private readonly IDonateAnywhereService _donateAnywhereService;

        public ButtonController()
            : this(new DonateAnywhereRequestContextFactory(),
                   new DonateAnywhereService(new PageAnalyser(new DirectHttpRequestTransport(), new SimpleKeywordRankingStrategy(), new ContentCleaner()), 
                   new ApiSearchProvider()))
        {
        }

        public ButtonController(IDonateAnywhereRequestContextFactory contextFactory, IDonateAnywhereService donateAnywhereService)
        {
            _contextFactory = contextFactory;
            _donateAnywhereService = donateAnywhereService;
        }

        public ActionResult Index()
        {
            var donateAnywhereContext = _contextFactory.BuildContext(HttpContext);
            if(!donateAnywhereContext.EnoughInformationToBuildSuggestions)
            {
                return RedirectToAction("NotEnoughInformation");
            }

            var donateAnywhereResult = _donateAnywhereService.EvaluateRequest(donateAnywhereContext);

            if(donateAnywhereResult.Results.Count == 0)
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
