namespace Plexito.JavaScriptLogic.Stubs
{
    using System;
    using System.Net;
    using System.Xml.Linq;

    public class XMLHttpRequest
    {
        private string verb;

        private string url;

        private WebClient wc;

        public void open(string verb, string url, bool async)
        {
            if (async)
            {
                throw new NotImplementedException();
            }
            if (!verb.Equals("get", StringComparison.OrdinalIgnoreCase))
            {
                
            }
            this.url = url;
            this.wc = new WebClient();
        }

        public void setRequestHeader(string headerName, string value)
        {
            wc.Headers.Add(headerName,value);
        }

        public void send()
        {
            try
            {
                this.responseXML = wc.DownloadString(this.url);
            }
            catch (WebException e)
            {
                this.status = (ushort)((HttpWebResponse)(e.Response)).StatusCode;
                return;
            }
            this.status = 200;
        }

        public int status { get; set; }

        public string responseXML { get; set; }
    }


}