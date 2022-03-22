using Amazon.Tasks.SpecFlow.Drivers;
using OpenQA.Selenium;

namespace Amazon.Tasks.SpecFlow.Hooks
{
    [Binding]
    public sealed class HookAmazonProducts
    {
        private readonly ScenarioContext _scenarioContext;

        public HookAmazonProducts(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

        [BeforeScenario("OneProductTest")]
        [BeforeScenario("MultipleProductsTest")]
        public void BeforeScenario()
        {
            var seleniumDriver = new SeleniumDriver(_scenarioContext);
            _scenarioContext.Set(seleniumDriver, "SeleniumDriver");
        }

        [AfterScenario("OneProductTest")]
        [AfterScenario("MultipleProductsTest")]
        public void AfterScenario()
        {
           _scenarioContext.Get<IWebDriver>("WebDriver").Quit();
        }
    }
}