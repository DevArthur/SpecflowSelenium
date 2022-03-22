using Amazon.Tasks.SpecFlow.Drivers;
using Amazon.Tasks.SpecFlow.PagesObject;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Amazon.Tasks.StepDefinitions
{
    [Binding]
    [Parallelizable(ParallelScope.Self)]
    public class AmazonStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly AmazonHomePage amazonHomePage;
        private readonly AmazonResultsPage amazonResultsPage;
        private readonly AmazonProductDetailPage amazonProductDetailPage;
        private readonly AmazonCartPage amazonCartPage;

        public AmazonStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = _scenarioContext.Get<SeleniumDriver>("SeleniumDriver").Setup();
            amazonHomePage = new AmazonHomePage(_driver);
            amazonResultsPage = new AmazonResultsPage(_driver);
            amazonProductDetailPage = new AmazonProductDetailPage(_driver);
            amazonCartPage = new AmazonCartPage(_driver);
        }

        [Given(@"I navigate to amazon web site in Mexico")]
        public void GivenINavigateToAmazonWebSiteInMexico()
        {
            _driver.Url = amazonHomePage.GetUrl();
        }

        [When(@"I search for a product? (.*)")]
        public void GivenISearchForAProduct(string product)
        {
            amazonHomePage.GetProduct().SendKeys(product);
        }

        [When(@"I click on search button")]
        public void GivenIClickOnSearchButton()
        {
            amazonHomePage.GetSearchButton().Click();
        }

        [When(@"I select sort by from lowest to top price product")]
        public void GivenISelectSortByFromLowestToTopPriceProduct()
        {
            amazonResultsPage.GetDropDownListFilter().Click();
        }

        [When(@"I select the first product in the list")]
        public void GivenISelectTheFirstProductInTheList()
        {
            amazonResultsPage.GetDropDownListFiltered().First().Click();
        }

        [When(@"I click on Add to cart button")]
        public void WhenIClickOnAddToCartButton()
        {   
            var expectedProduct = amazonProductDetailPage.GetProductTitle().Text;
            _scenarioContext.Add("expectedProduct", expectedProduct);
            amazonProductDetailPage.GetAddToCartButton().Click();
        }

        [When(@"I go to the cart")]
        public void WhenIGoToTheCart()
        {
            amazonProductDetailPage.GetGoToCartButton().Click();
        }

        [Then(@"I verify the product? selected is added in the cart")]
        public void ThenIVerifyTheProductSelectedIsAddedInTheCart()
        {
            Assert.AreEqual(_scenarioContext.Get<string>("expectedProduct"), amazonCartPage.GetProductName().Text);
        }
    }
}