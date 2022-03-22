using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Amazon.Tasks.SpecFlow.PagesObject
{
    public class AmazonProductDetailPage
    {
        private readonly IWebDriver _driver;
        public AmazonProductDetailPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "add-to-cart-button")]
        private readonly IWebElement? _addToCartButton;

        [FindsBy(How = How.XPath, Using = "//span[@id='attach-sidesheet-view-cart-button']/span/input")]
        private readonly IWebElement? _goToCartButton;

        [FindsBy(How = How.Id, Using = "productTitle")]
        private IWebElement? _productTitle;

        public IWebElement? GetAddToCartButton() => _addToCartButton;
        public IWebElement? GetGoToCartButton() => _goToCartButton;
        public IWebElement? GetProductTitle() => _productTitle;
    }
}
