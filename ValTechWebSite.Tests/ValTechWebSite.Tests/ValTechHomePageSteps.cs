using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using Shouldly;
using TechTalk.SpecFlow;

namespace ValTechWebSite.Tests
{
    [Binding]
    public class ValTechHomePageSteps
    {
        private IWebDriver _driver;
        private ValTechHomePage _valTechHomePage;

        [Given(@"I am on the hompage")]
        public void GivenIAmOnTheHompage()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _valTechHomePage = ValTechHomePage.NavigateToHomePage(_driver);
            PageFactory.InitElements(_driver, _valTechHomePage);
        }

        [Then(@"the latest news section is displayed")]
        public void ThenTheLatestNewsSectionIsDisplayed()
        {
            _valTechHomePage.LatestNews.Displayed.ShouldBeTrue();
        }

        [AfterScenario]
        public void CloseWebDriver()
        {
            _driver.Dispose();
        }
    }
}
