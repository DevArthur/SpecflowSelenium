using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Amazon.Tasks.SpecFlow.PagesObject
{
    public class AmazonHomePage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "https://www.amazon.com.mx/";
        public AmazonHomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "twotabsearchtextbox")]
        private readonly IWebElement? _product;
        [FindsBy(How = How.Id, Using = "nav-search-submit-button")]
        private readonly IWebElement? _searchButton;
        
        public string GetUrl() => _url;
        public IWebElement? GetProduct() => _product;
        public IWebElement? GetSearchButton() => _searchButton;
    }
}
