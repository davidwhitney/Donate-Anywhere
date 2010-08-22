﻿using System;
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
    public class ButtonController : Controller
    {
        private readonly IDonateAnywhereRequestContextFactory _contextFactory;
        private readonly IDonateAnywhereService _donateAnywhereService;

        public ButtonController()
            : this(new DonateAnywhereRequestContextFactory(), 
                   new DonateAnywhereService(new PageAnalyser(new DirectHttpRequestTransport(), new SimpleKeywordRankingTextAnalyser()), 
                   new MockSearchProvider()))
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