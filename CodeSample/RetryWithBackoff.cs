using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeSample
{
    public class RetryWithBackoff
    {
        private static readonly int MAX_RETRIES = 3;
        private const string _baseURL = "<replace with API Gateway URL>";
        private HttpClient _client;
        public RetryWithBackoff()
        { 
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.TransferEncodingChunked = false;
            
        }
        
        public async Task DoRetriesWithBackOff()
        {
            int retries = 0;
            bool retry;
            do
            {
                //Sample object for sending parameters
                var parameterObj = new InputParameter { SimulateTimeout = "false" };
                var content = new StringContent(JsonConvert.SerializeObject(parameterObj), System.Text.Encoding.UTF8,
                    "application/json");
                var waitInMilliseconds = Convert.ToInt32((Math.Pow(2, retries) - 1) * 100);
                System.Threading.Thread.Sleep(waitInMilliseconds);
                var response =  await _client.PostAsync(_baseURL, content);
                switch (response.StatusCode)
                {
                    //Success
                    case HttpStatusCode.OK:
                        retry = false;
                        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                        break;
                    //Throttling, timeouts 
                    case HttpStatusCode.TooManyRequests:
                    case HttpStatusCode.GatewayTimeout:
                        retry = true;
                        break;
                    //Some other error occured, so stop calling the API
                    default:
                        retry = false;
                        break;
                }
                retries++;
            } while (retry && retries < MAX_RETRIES);
        }
    }

    public class InputParameter
    {
        public string SimulateTimeout { get; set; }
    }
}