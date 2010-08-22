using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GG.DonateAnywhere.Code;
using GG.DonateAnywhere.Core;
using GG.DonateAnywhere.Core.Http;
using GG.DonateAnywhere.Core.PageAnalysis;

namespace GG.DonateAnywhere.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly IDonateAnywhereRequestContextFactory _contextFactory;
        private readonly IDonateAnywhereService _donateAnywhereService;

        public LandingPageController()
            : this(new DonateAnywhereRequestContextFactory(), 
                   new DonateAnywhereService(new PageAnalyser(new DirectHttpRequestTransport(), new SimpleKeywordRankingStrategy()), 
                   new MockSearchProvider()))
        {
        }

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
