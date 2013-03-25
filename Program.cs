using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DomainWebProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            //var password = new SecureString();
            //"myADdomainPassword".ToCharArray().ToList().ForEach(p => password.AppendChar(p));
            //var credentials = new NetworkCredential("aluqman", password, "ad");

            var uri = new Uri("http://proxy-name:8080");

            var proxy = new WebProxy(uri, true, null);
            proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //PrintURLWebClient(proxy);
            PrintURLHttpWebRequest(proxy);
        }

        private static void PrintURLHttpWebRequest(WebProxy proxy)
        {
            WebRequest request = WebRequest.Create("http://www.google.com");
            request.Proxy = proxy;
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string downloadString = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                downloadString = sr.ReadToEnd();
            }
            Console.WriteLine(downloadString);
        }

        private static void PrintURLWebClient(WebProxy proxy)
        {
            var client = new WebClient();
            client.Proxy = proxy;
            string downloadString = client.DownloadString("http://www.google.com");

            Console.WriteLine(downloadString);
        }
    }
}
