using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V106.Network;
using QatlPageTests.PageObjects;

namespace QatlPageTests
{
    public class Tests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
            _webDriver.Navigate().GoToUrl("http://shop.qatl.ru");
            _webDriver.Manage().Window.Maximize();
        }

        [Test]
        public void LoginAsUser_StandardBehavior_Logined()
        {
            var mainMenu = new MainMenuPageObject(_webDriver);

            mainMenu
                .DropdownClick()
                .SignIn()
                .Login(TestKits.UserLogin, TestKits.UserPassword);

            Assert.That(new AuthorizationPageObject(_webDriver).GetAlertSuccessText, 
                Is.EqualTo(TestKits.ExpectedAlertAuthText), "The login was not executed");
        }

        [Test]
        public void ProductSearch_StandardBehavior_Found()
        {
            var mainMenu = new MainMenuPageObject(_webDriver);

            mainMenu.EnterASearch(TestKits.WatchName);
            var actualHintText = mainMenu.GetSearchHintText();
            var actualRequestText = mainMenu
                .ClickSearchButton()
                .GetRequestText();

            var catalog = new CatalogPageObject(_webDriver);
            var actualWatchName = catalog
                .ClickMen()
                .ClickCategoryPage(3)
                .ClickCategoryPage(4)
                .ClickCasioGA()
                .GetWatchName();

            var product = new ProductPageObject(_webDriver);
            var actualWatchPrice = product.GetWatchPrice();

            Assert.That(actualHintText, Is.EqualTo(TestKits.WatchName), "No such hint was found");
            Assert.True(actualRequestText.Contains(TestKits.WatchName), "The search is not working correctly");
            Assert.That(actualWatchName, Is.EqualTo(TestKits.WatchName), "Wrong name or it doesn't exist at all");
            Assert.That(actualWatchPrice, Is.EqualTo(TestKits.WatchPrice), "The wrong price or there is none at all");
        }

        [Test]
        public void AddingToCart_StandardBehavior_Added()
        {
            var mainMenu = new MainMenuPageObject(_webDriver);

            var expectedWatchPrice = mainMenu.GetWatchPrice(TestKits.WatchId);
            var actualNumberOfProducts = mainMenu
                .AddToCart(TestKits.WatchId)
                .GetNumberOfProducts();

            var cart = new CartPageObject(_webDriver);
            var actualCartSum = cart.GetCartSum();

            Assert.That(actualCartSum, Is.EqualTo(expectedWatchPrice), "The product was not added or the amount is calculated incorrectly");
            Assert.That(actualNumberOfProducts, Is.EqualTo(TestKits.NumberOfProducts), "The product was not added or the number is calculated incorrectly");
        }

        [Test]
        public void MakingAnOrder_StandardBehavior_Ordered()
        {
            var mainMenu = new MainMenuPageObject(_webDriver);

            var actualAlertOrderedtext = mainMenu
                .DropdownClick()
                .SignIn()
                .Login(TestKits.UserLogin, TestKits.UserPassword)
                .BackToMainMenu()
                .AddToCart(TestKits.WatchId)
                .MakeOrder()
                .PlaceAnOrder()
                .GetAlertSuccessText();

            Assert.That(actualAlertOrderedtext, Is.EqualTo(TestKits.ExpectedAlertOrderedText), "The order was not placed or something happened");
        }

        [TearDown]
        public void TearDown()
        {
            //_webDriver.Quit();
        }
    }
}