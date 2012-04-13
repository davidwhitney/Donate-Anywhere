using DonateAnywhere.Web.Code;
using GG.DonateAnywhere.Core;
using GG.DonateAnywhere.Core.Http;
using GG.DonateAnywhere.Core.PageAnalysis;
using GG.DonateAnywhere.Core.Sanitise;
using GG.DonateAnywhere.Core.Searching;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DonateAnywhere.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DonateAnywhere.Web.App_Start.NinjectWebCommon), "Stop")]

namespace DonateAnywhere.Web.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDonateAnywhereRequestContextFactory>().To<DonateAnywhereRequestContextFactory>();
            kernel.Bind<IDonateAnywhereService>().To<DonateAnywhereService>();
            
            kernel.Bind<Cache>().ToSelf().InSingletonScope();

            kernel.Bind<IDirectHttpRequestTransport>().To<CachingHttpGetter>();
            kernel.Bind<IDirectHttpRequestTransport>().To<DirectHttpRequestTransport>().WhenInjectedInto<CachingHttpGetter>();

            kernel.Bind<IPageAnalyser>().To<PageAnalyser>();

            kernel.Bind<ISearchProvider>().To<CachingApiSearchProvider>();
            kernel.Bind<ISearchProvider>().To<ApiSearchProvider>().WhenInjectedInto<CachingApiSearchProvider>();

            kernel.Bind<IKeywordRankingStrategy>().To<SimpleKeywordRankingStrategy>();
            kernel.Bind<ContentCleaner>().ToSelf();
            kernel.Bind<IExcludedWordsRepository>().ToMethod(x => new AssemblyResourceExcludedWordsRepository("GG.DonateAnywhere.Core.PageAnalysis.blacklist.txt")).InSingletonScope();
        }        
    }
}
