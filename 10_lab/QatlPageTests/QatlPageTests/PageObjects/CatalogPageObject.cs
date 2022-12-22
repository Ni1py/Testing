using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QatlPageTests.PageObjects
{
    internal class CatalogPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _requestText = By.XPath("//ol[@class='breadcrumb']//li[2]");
        private readonly By _menButton = By.XPath("//a[@href='category/men']");
        private readonly By _casioGAButton = By.XPath("//a[@href='product/casio-ga-1000-1aer-3']");
        private const string _categoryPages = "//a[@href='/category/men?page=";
        private const string _categoryPagesEnd = "']";

        public CatalogPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetRequestText()
        {
            return _webDriver.FindElement(_requestText).Text;
        }

        public CatalogPageObject ClickMen()
        {
            _webDriver.FindElement(_menButton).Click();

            return new CatalogPageObject(_webDriver);
        }

        public CatalogPageObject ClickCategoryPage(int number)
        {
            _webDriver.FindElement(By.XPath(_categoryPages + number + _categoryPagesEnd)).Click();

            return new CatalogPageObject(_webDriver);
        }

        public ProductPageObject ClickCasioGA()
        {
            _webDriver.FindElement(_casioGAButton).Click();

            return new ProductPageObject(_webDriver);
        }
    }
}
