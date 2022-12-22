using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QatlPageTests.PageObjects
{
    internal class MainMenuPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _dropdownButton = By.XPath("//a[@class='dropdown-toggle']");
        private readonly By _signInButton = By.XPath("//a[@href='user/login']");
        private readonly By _searchInputButton = By.XPath("//input[@class='typeahead tt-input']");
        private readonly By _loupeButton = By.XPath("//span[@class='twitter-typeahead']/following-sibling::input");
        private readonly By _searchHint = By.XPath("//strong[@class='tt-highlight']");
        private const string _addToCart = "//a[@href='cart/add?id=";
        private const string _watchPrice = "']/following-sibling::span";

        public MainMenuPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public MainMenuPageObject DropdownClick()
        {
            _webDriver.FindElement(_dropdownButton).Click();

            return new MainMenuPageObject(_webDriver);
        }

        public AuthorizationPageObject SignIn()
        {
            _webDriver.FindElement(_signInButton).Click();

            return new AuthorizationPageObject(_webDriver);
        }

        public MainMenuPageObject EnterASearch(string name)
        {
            _webDriver.FindElement(_searchInputButton).SendKeys(name);

            return new MainMenuPageObject(_webDriver);
        }

        public string GetSearchHintText()
        {
            Thread.Sleep(2000);
            return _webDriver.FindElement(_searchHint).Text;
        }

        public CatalogPageObject ClickSearchButton()
        {
            _webDriver.FindElement(_loupeButton).Click();

            return new CatalogPageObject(_webDriver);
        }

        public string GetWatchPrice(int watchId)
        {
            return _webDriver.FindElement(By.XPath(_addToCart + watchId + _watchPrice)).Text;
        }

        public CartPageObject AddToCart(int watchId)
        {
            _webDriver.FindElement(By.XPath(_addToCart + watchId + "\']")).Click();

            return new CartPageObject(_webDriver);
        }
    }
}
