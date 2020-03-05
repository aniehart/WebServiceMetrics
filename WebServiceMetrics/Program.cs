using System;

namespace WebServiceMetrics
{
    using System.Net.Http;

    class Program
    {
        static void Main(string[] args)
        {
            //parameters
            //# of times to try
            //request body
            //URL to hit

            //Load sample request
            //start time
            //post request
            //stop time
            //log results
            var httpClient = new HttpClient();
            var response = httpClient.PostAsync("", new StringContent())
        }
    }
}
