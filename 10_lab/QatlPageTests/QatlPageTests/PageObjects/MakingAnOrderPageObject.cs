using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QatlPageTests.PageObjects
{
    internal class MakingAnOrderPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _placeAnOrderButton = By.XPath("//button[@class='btn btn-default']");
        private readonly By _alertSuccess = By.XPath("//div[@class='alert alert-success']");

        public MakingAnOrderPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public MakingAnOrderPageObject PlaceAnOrder()
        {
            _webDriver.FindElement(_placeAnOrderButton).Click();

            return new MakingAnOrderPageObject(_webDriver);
        }

        public string GetAlertSuccessText()
        {
            return _webDriver.FindElement(_alertSuccess).Text;
        }
    }
}
