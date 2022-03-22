using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace Amazon.Tasks.SpecFlow.Drivers
{
    public class SeleniumDriver
    {
        private readonly ThreadLocal<IWebDriver>? _driver = new();

        private readonly ScenarioContext _scenarioContext;

        public SeleniumDriver(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

        [SetUp]
        public IWebDriver Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            _driver.Value = new ChromeDriver();
            _scenarioContext.Set(_driver.Value, "WebDriver");
            _driver.Value.Manage().Window.Maximize();
            _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return _driver.Value;
        }
    }
}
