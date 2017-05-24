using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HJCS.SageAssessment.ClientMVC.AppCode
{
    public class DataRetriever
    {
        internal static string GetStringAsync(string url)
        {
            var responseString = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                }
            }
            return responseString;
        }
    }
}
