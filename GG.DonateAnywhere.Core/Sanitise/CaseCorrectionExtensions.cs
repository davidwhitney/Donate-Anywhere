using System;
using System.Text;

namespace GG.DonateAnywhere.Core.Sanitise
{
    public static class CaseCorrectionExtensions
    {
        public static string ToProperCase(this string stringInput)
        {        
            var sb = new StringBuilder();
            var fEmptyBefore = true;
            foreach (var ch in stringInput)
            {
                var chThis = ch;
                if (Char.IsWhiteSpace(chThis))
                {
                    fEmptyBefore = true;
                }
                else
                {
                    if (Char.IsLetter(chThis) && fEmptyBefore)
                    {
                        chThis = Char.ToUpper(chThis);
                    }
                    else
                    {
                        chThis = Char.ToLower(chThis);
                    }
                    fEmptyBefore = false;
                }
                sb.Append(chThis);
            }
            return sb.ToString();
            
        }
    }
}
