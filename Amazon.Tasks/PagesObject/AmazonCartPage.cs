using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Amazon.Tasks.SpecFlow.PagesObject
{
    public class AmazonCartPage
    {
        private readonly IWebDriver _driver;
        
        public AmazonCartPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.XPath, Using ="//span[@class='a-truncate-cut']")]
        private readonly IWebElement? _productName;

        [FindsBy(How = How.XPath, Using = "//span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap sc-product-price a-text-bold']")]
        private readonly IWebElement? _productPrice;

        public IWebElement? GetProductName() => _productName;
        public IWebElement? GetProductPrice() => _productPrice;
    }
}

