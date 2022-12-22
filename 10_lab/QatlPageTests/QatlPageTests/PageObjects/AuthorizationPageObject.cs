using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QatlPageTests.PageObjects
{
    internal class AuthorizationPageObject
    {
        private IWebDriver _webDriver;

        private readonly By _loginInputButton = By.XPath("//input[@name='login']");
        private readonly By _passwordInputButton = By.XPath("//input[@name='password']");
        private readonly By _submitButton = By.XPath("//button[@type='submit']");
        private readonly By _alertSuccess = By.XPath("//div[@class='alert alert-success']");
        private readonly By _mainMenuButton = By.XPath("//a[@href='http://shop.qatl.ru']");

        public AuthorizationPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public AuthorizationPageObject Login(string login, string password)
        {
            _webDriver.FindElement(_loginInputButton).SendKeys(login);
            _webDriver.FindElement(_passwordInputButton).SendKeys(password);
            _webDriver.FindElement(_submitButton).Click();

            return new AuthorizationPageObject(_webDriver);
        }

        public string GetAlertSuccessText()
        {
            return _webDriver.FindElement(_alertSuccess).Text;
        }

        public MainMenuPageObject BackToMainMenu()
        {
            _webDriver.FindElement(_mainMenuButton).Click();

            return new MainMenuPageObject(_webDriver);
        }
    }
}
