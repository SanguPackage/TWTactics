using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TribalWars.Tools
{
    /// <summary>
    /// Interact met the network and set proxy
    /// on requests if necessary
    /// </summary>
    public static class Network
    {
	    private static IWebProxy GetProxy()
	    {
		    IWebProxy proxy;
		    if (Properties.Settings.Default.Proxy)
		    {
			    try
			    {
					proxy = new WebProxy(Properties.Settings.Default.ProxyAddress + ":" + Properties.Settings.Default.ProxyPort);
			    }
			    catch
			    {
					proxy = WebRequest.GetSystemWebProxy();
			    }
		    }
		    else
		    {
			    proxy = WebRequest.GetSystemWebProxy();
		    }

			proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
		    return proxy;
	    }

        public static void PostValues(string url, NameValueCollection data)
        {
            using (var wb = new WebClient())
            {
	            wb.Proxy = GetProxy();
                var response = wb.UploadValues(url, "POST", data);
            }
        }

        public static WebRequest CreateWebRequest(string url)
        {
            var client = WebRequest.Create(url);
	        client.Proxy = GetProxy();
            return client;
        }

        public static string GetWebRequest(string url)
        {
            WebRequest request = CreateWebRequest(url);
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static XDocument DownloadXml(string url)
        {
            string xml = GetWebRequest(url);
            return XDocument.Parse(xml);
        }
    }
}
