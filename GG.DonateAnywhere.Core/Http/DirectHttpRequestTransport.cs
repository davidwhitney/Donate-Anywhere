using System;
using System.IO;
using System.Net;

namespace GG.DonateAnywhere.Core.Http
{
    public class DirectHttpRequestTransport : IDirectHttpRequestTransport
    {
        public string FetchUri(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri.ToString());

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseStream = response.GetResponseStream();

                if(responseStream == null)
                {
                    throw new Exception("Response stream is null.");
                }

                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}