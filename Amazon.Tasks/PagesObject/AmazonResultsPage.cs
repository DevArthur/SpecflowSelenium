using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Amazon.Tasks.SpecFlow.PagesObject
{
    public class AmazonResultsPage
    {
        private readonly IWebDriver _driver;
        public AmazonResultsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//option[contains(text(),'Precio: de más bajo a más alto')]")]
        private readonly IWebElement? _dropDownListFilter;

        [FindsBy(How = How.ClassName, Using = "a-size-base-plus")]
        private readonly IList<IWebElement?> _dropDownListFiltered;

        public IWebElement? GetDropDownListFilter() => _dropDownListFilter;
        public IList<IWebElement?> GetDropDownListFiltered() => _dropDownListFiltered; 
    }
}
