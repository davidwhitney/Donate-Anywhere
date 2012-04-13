using System.Web;
using GG.DonateAnywhere.Core;

namespace DonateAnywhere.Web.Code
{
    public interface IDonateAnywhereRequestContextFactory
    {
        IDonateAnywhereRequestContext BuildContext(HttpContextBase httpContextBase);
    }
}