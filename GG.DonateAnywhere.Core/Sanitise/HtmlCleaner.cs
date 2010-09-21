using HtmlAgilityPack;

namespace GG.DonateAnywhere.Core.HtmlCleaner
{
    public class HtmlCleaner
    {
        public string RemoveHtml(string source)
        {
            var textWhichMayContainHtml = new HtmlDocument();
            textWhichMayContainHtml.LoadHtml(source);
            return textWhichMayContainHtml.DocumentNode.InnerText;
        }
    }
}
