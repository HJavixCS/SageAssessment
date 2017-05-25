using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HJCS.SageAssessment.ClientMVC.AppCode
{
    public class HttpClientHelper
    {
        internal static async Task<HttpResponseMessage> GetStringAsync(string uri)
        {
            var responseString = string.Empty;
            using (var httpClient = new HttpClient())
            {
                InitializeHttpClient(httpClient, uri);

                return await httpClient.GetAsync(uri);
            }
        }

        internal static async Task<HttpResponseMessage> PostAsync(string url, string data) 
        {
            using (var httpClient = new HttpClient())
            {
                InitializeHttpClient(httpClient, url);

                var byteContent = GetByteContent(data);

                var response = await httpClient.PostAsync(url, byteContent);

                return response;
            }
        }

        internal static async Task<HttpResponseMessage> PutAsync(string url, string data)
        {
            using (var httpClient = new HttpClient())
            {
                InitializeHttpClient(httpClient, url);

                var byteContent = GetByteContent(data);

                var response = await httpClient.PutAsync(url, byteContent);

                return response;
            }
        }

        private static void InitializeHttpClient(HttpClient client, string uri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static ByteArrayContent GetByteContent(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }
    }
}
