using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Test.Tasks.Models;

namespace Test.Tasks.StepDefinitions
{
    [Binding]
    public class DatabaseDataStepDefinitions
    {
        private readonly HttpClient _client = new();
        private readonly LibraryDataBaseContext _databaseContext = new();
        private readonly ScenarioContext _scenarioContext;        
        public DatabaseDataStepDefinitions(ScenarioContext scenarioContext)
        {            
            _scenarioContext = scenarioContext;            
        }

        [When(@"I make a http Get request to (.*)")]
        public void WhenIMakeAHttpGetRequestToHttpsLocalhostBooks(string url)
        {
            var GetData = JsonConvert.DeserializeObject<List<Book>>(_client.GetStringAsync(url).Result);
            _scenarioContext.Set(GetData, "GetApiData");
        }

        [Then(@"I query the database with statement (.*)")]
        public void ThenIQueryTheDatabaseWithStatementStatement(string statement)
        {
            var databaseData = _databaseContext.Books.FromSqlRaw(statement).ToList();
            _scenarioContext.Set(databaseData, "GetDatabaseData");
        }

        [Then(@"compare the results\.")]
        public void ThenCompareTheResults_()
        {
            var apiData = _scenarioContext.Get<List<Book>>("GetApiData");
            var databaseData = _scenarioContext.Get<List<Book>>("GetDatabaseData");
            apiData.Should().BeEquivalentTo(databaseData);            
        }

        [When(@"I make a http Post request to (.*) response should be not null")]
        public void WhenIMakeAHttpPostRequestResponseShouldBeNotNull(string url)
        {
            var filePathAndName = Path.GetFullPath("@/../../../../utilities/AnimalDataSources/HttpPOSTData.json");
            var json = JToken.Parse(File.ReadAllText(filePathAndName));
            var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, content).Result;
            response.Should().NotBeNull();
        }

        [When(@"retrieve the Id inserted with statement (.*)")]
        public void WhenRetrieveTheIdInsertedWithStatement(string statement1)
        {
            var data = _databaseContext.Books.FromSqlRaw(statement1).ToList();            
            _scenarioContext.Set(data[0].Id, "Id");
        }

        [Then(@"I query the database to get api inserted data with statement (.*)")]
        public void ThenIQueryTheDatabaseToGetApiInsertedDataWithStatement(string statement2)
        {
            var id = _scenarioContext.Get<int>("Id");
            var data = _databaseContext.Books.FromSqlRaw($"{statement2} {id}");
            Assert.NotNull(data);
        }

        [When(@"I make a http Put request to (.*)")]
        public void WhenIMakeAHttpPutRequestResponseShouldBeNotNull(string url)
        {
            var filePathAndName = Path.GetFullPath("@/../../../../utilities/AnimalDataSources/HttpPUTData.json");
            var json = JToken.Parse(File.ReadAllText(filePathAndName));
            var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = _client.PutAsync(url, content).Result;
            response.Should().NotBeNull();
            var bookToUpdate = JsonConvert.DeserializeObject<Book>(json.ToString());
            _scenarioContext.Set(bookToUpdate, "bookToUpdate");
        }

        [Then(@"Data must be updated when query the database with statement (.*)")]
        public void ThenDataMustBeUpdatedWhenQueryTheDatabaseWithStatement(string statement)
        {            
            var bookToUpdate = _scenarioContext.Get<Book>("bookToUpdate");
            var bookUpdated = _databaseContext.Books.FromSqlRaw($"{statement}");
            Assert.AreEqual(bookToUpdate.Editorial, bookUpdated.Select(x => x.Editorial).FirstOrDefault());
        }

        [When(@"I make a http Delete request to (.*)")]
        public void WhenIMakeAHttpDeleteRequestToUrl(string url)
        {
            _client.DeleteAsync(url);
        }

        [Then(@"Record not must be in database when query the database with statement (.*)")]
        public void ThenRecordNotMustBeInDatabaseWhenQueryTheDatabaseWithStatement(string statement)
        {
            var result = _databaseContext.Books.FromSqlRaw($"{statement}");
            Assert.IsEmpty(result);
        }
    }
}
