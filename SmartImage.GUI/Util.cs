using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SmartImage.GUI
{
	internal class Util
	{

		//https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
		public static string Get(string uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			//https://stackoverflow.com/questions/51979773/http-post-to-api-return-403-forbidden
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
    }
}
