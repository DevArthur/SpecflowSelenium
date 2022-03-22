namespace Amazon.Tasks.SpecFlow.Utilities
{
    public class HttpClientRequest
    {        
        private static readonly HttpClient _client = new();

        public async Task<string> GetRequestAsync(string url)
        {
            try
            {
                return await _client.GetStringAsync(url);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"{e.Message}");
            }
        }

        public async Task<HttpResponseMessage> PostRequestAsync(string url, StringContent content)
        {
            try
            {
                return await _client.PostAsync(url, content);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"{e.Message}");
            }
        }

        public async Task<HttpResponseMessage> PutRequestAsync(string url, HttpContent content)
        {
            try
            {
                return await _client.PutAsync(url, content);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"{e.Message}");
            }
        }

        public async Task<HttpResponseMessage> PatchRequestAsync(string url, HttpContent content)
        {
            try
            {
                return await _client.PatchAsync(url, content);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"{e.Message}");
            }
        }

        public async Task<HttpResponseMessage> DeleteRequestAsync(string url)
        {
            try
            {
                return await _client.DeleteAsync(url);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException($"{e.Message}");
            }
        }
    }
}
