using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QatlPageTests.PageObjects
{
    internal class ProductPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _watchNameHeader = By.XPath("//div[@class='single-para simpleCart_shelfItem']/h2");
        private readonly By _watchPriceHeader = By.XPath("//div[@class='single-para simpleCart_shelfItem']/h5");

        public ProductPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetWatchName()
        {
            return _webDriver.FindElement(_watchNameHeader).Text;
        }

        public string GetWatchPrice()
        {
            return _webDriver.FindElement(_watchPriceHeader).Text;
        }
    }
}
