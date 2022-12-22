using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QatlPageTests.PageObjects
{
    internal class CartPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _numberOfProductsTd = By.XPath("//td[@class='text-right cart-qty']");
        private readonly By _cartSumTd = By.XPath("//td[@class='text-right cart-sum']");
        private readonly By _makeAnOrderButton = By.XPath("//a[@href='cart/view']");

        public CartPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetNumberOfProducts()
        {
            Thread.Sleep(4000);
            return _webDriver.FindElement(_numberOfProductsTd).Text;
        }

        public string GetCartSum()
        {
            Thread.Sleep(4000);
            return _webDriver.FindElement(_cartSumTd).Text;
        }

        public MakingAnOrderPageObject MakeOrder()
        {
            Thread.Sleep(2000);
            _webDriver.FindElement(_makeAnOrderButton).Click();

            return new MakingAnOrderPageObject(_webDriver);
        }
    }
}
