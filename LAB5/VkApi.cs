using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace LAB5
{
    class VkApi
    {
        public string Get(string URL, string method, string token, string parameter = "")
        {
            WebRequest request = WebRequest.Create(String.Format(URL, method, token) + "&" + parameter);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
