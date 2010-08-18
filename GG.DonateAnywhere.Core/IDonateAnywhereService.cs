namespace GG.DonateAnywhere.Core
{
    public interface IDonateAnywhereService
    {
        DonateAnywhereResult EvaluateRequest(IDonateAnywhereRequestContext donateAnywhereContext);
    }
}