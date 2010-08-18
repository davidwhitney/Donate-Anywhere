using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GG.DonateAnywhere.Core.Http
{
    public interface IDirectHttpRequestTransport
    {
        string FetchUri(Uri uri);
    }
}
