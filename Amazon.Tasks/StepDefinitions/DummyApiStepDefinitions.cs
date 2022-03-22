using Amazon.Tasks.SpecFlow.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;

namespace Amazon.Tasks.SpecFlow.StepDefinitions
{
    [Binding]
    [Parallelizable(ParallelScope.Self)]
    public class DummyApiStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClientRequest _httpRequest;

        public DummyApiStepDefinitions(ScenarioContext context)
        {
            _scenarioContext = context;
            _httpRequest = new HttpClientRequest();           
        }

        [When(@"I perform a http request (.*) (.*)")]
        public void WhenIPerformHttpRequest(string url, string httpRequestType)
        {
            string filePathAndName;
            StringContent content;
            HttpResponseMessage? response = null;
            switch (httpRequestType.ToUpper())
            {
                case "GET":
                    var jsonReceived = _httpRequest.GetRequestAsync(url).Result;
                    _scenarioContext.Set(jsonReceived.ToString(), "jsonReceived");
                    break;
                case "POST":
                    filePathAndName = Path.GetFullPath("@/../../../../utilities/DataSources/HttpPostRequestContent.json");
                    content = new StringContent(File.ReadAllText(filePathAndName));                    
                    response = _httpRequest.PostRequestAsync(url, content).Result;
                    break;
                case "PUT":
                    filePathAndName = Path.GetFullPath("@/../../../../utilities/DataSources/HttpPutRequestContent.json");
                    content = new StringContent(File.ReadAllText(filePathAndName));
                    response = _httpRequest.PutRequestAsync(url, content).Result;                   
                    break;
                case "PATCH":
                    filePathAndName = Path.GetFullPath("@/../../../../utilities/DataSources/HttpPatchRequestContent.json");
                    content = new StringContent(File.ReadAllText(filePathAndName));
                    response = _httpRequest.PatchRequestAsync(url, content).Result;                    
                    break;
                case "DELETE":
                    response = _httpRequest.DeleteRequestAsync(url).Result;                   
                    break;
            }
        }

        [Then(@"I receive a response that looks like (.*) (.*)")]
        public void ThenIReceiveAResponseThatLooksLike(string statusCode, string httpRequestType)
        {
            switch (httpRequestType.ToUpper())
            {
                case "GET":
                    var filePathAndName = Path.GetFullPath("@/../../../../utilities/DataSources/HttpGetRequestExpectedData.json");
                    var jsonExpected = JToken.Parse(File.ReadAllText(filePathAndName));                    
                    var jsonReceived = JToken.Parse(_scenarioContext.Get<string>("jsonReceived"));
                    Assert.AreEqual(jsonExpected, jsonReceived);
                    break;
                case "POST":
                    statusCode.Should().Be(HttpStatusCode.Created.ToString());
                    break;
                case "PUT":
                case "PATCH":
                case "DELETE":
                    statusCode.Should().Be(HttpStatusCode.OK.ToString());
                    break;
            }
        }
    }
}
