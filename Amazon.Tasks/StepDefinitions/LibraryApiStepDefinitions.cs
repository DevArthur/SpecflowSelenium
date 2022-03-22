using Amazon.Tasks.SpecFlow.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;

namespace Test.Tasks.StepDefinitions
{
    [Binding]
    public  class LibraryApiStepDefinitions
    {
        private readonly HttpClientRequest _httpRequest;
        private readonly ScenarioContext _scenarioContext;

        public LibraryApiStepDefinitions(HttpClientRequest httpClient, ScenarioContext scenarioContext)
        {
            _httpRequest = httpClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"I make a http request (.*) to (.*)")]
        public void WhenIMakeAHttpRequestToHttpsLocalhostBooks(string request, string url)
        {
            JToken json;
            string filePathAndName;
            StringContent content;
            HttpResponseMessage? response = null;
            switch (request.ToUpper())
            {
                case "GET":
                    var jsonReceived = _httpRequest.GetRequestAsync(url).Result;
                    _scenarioContext.Set(jsonReceived.ToString(), "jsonReceived");
                    break;
                case "POST":
                    filePathAndName = Path.GetFullPath("@/../../../../utilities/AnimalDataSources/HttpPOSTData.json");
                    json = JToken.Parse(File.ReadAllText(filePathAndName));
                    content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
                    response = _httpRequest.PostRequestAsync(url, content).Result;
                    break;
                case "PUT":
                    filePathAndName = Path.GetFullPath("@/../../../../utilities/AnimalDataSources/HttpPUTData.json");
                    json = JToken.Parse(File.ReadAllText(filePathAndName));                    
                    content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
                    response = _httpRequest.PutRequestAsync(url, content).Result;
                    break;
                case "DELETE":
                    response = _httpRequest.DeleteRequestAsync(url).Result;
                    break;
            }
        }

        [Then(@"I get a response that looks like (.*) (.*)")]
        public void ThenIGetAResponseThatLooksLike(string request, int statusCode)
        {
            switch (request.ToUpper())
            {
                case "GET":
                    var filePathAndName = Path.GetFullPath("@/../../../../utilities/AnimalDataSources/HttpGETAllData.json");
                    var jsonExpected = JToken.Parse(File.ReadAllText(filePathAndName));
                    var jsonReceived = JToken.Parse(_scenarioContext.Get<string>("jsonReceived"));
                    Assert.AreEqual(jsonExpected, jsonReceived);
                    break;
                case "POST":
                    statusCode.Should().Be((int)HttpStatusCode.Created);
                    break;
                case "PUT":
                case "PATCH":
                case "DELETE":
                    statusCode.Should().Be((int)HttpStatusCode.OK);
                    break;
            }        
        }
    }
}
