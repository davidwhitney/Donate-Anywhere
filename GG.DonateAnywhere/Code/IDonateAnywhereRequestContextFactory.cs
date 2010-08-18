using System.Web;
using GG.DonateAnywhere.Core;

namespace GG.DonateAnywhere.Code
{
    public interface IDonateAnywhereRequestContextFactory
    {
        IDonateAnywhereRequestContext BuildContext(HttpContextBase httpContextBase);
    }
}