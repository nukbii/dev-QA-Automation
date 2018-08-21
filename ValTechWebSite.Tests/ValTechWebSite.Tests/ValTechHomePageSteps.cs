using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace ValTechWebSite.Tests
{
    [Binding]
    public class ValTechHomePageSteps
    {
        private IWebDriver _driver;
        private ValTechHomePage _valTechHomePage;
        private AboutPage _aboutPage;
        private WorkPage _workPage;
        private ServicesPage _servicesPage;
        private ContactUsPage _contactUsPage;
        private WrapperComponent _wrapperComponent;

        [BeforeScenario]
        public void InitDriver()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [Given(@"I am on the hompage")]
        public void GivenIAmOnTheHompage()
        {
            _valTechHomePage = ValTechHomePage.NavigateToHomePage(_driver);
            _valTechHomePage.AcceptCookies();
        }

        [Given(@"I am on the (.*) page")]
        public void GivenIAmOnThePage(string pageName)
        {
            _valTechHomePage = ValTechHomePage.NavigateToHomePage(_driver);
            _valTechHomePage.AcceptCookies();

            if (pageName.Equals(ValTechHomePage.ContactUs))
            {
                _contactUsPage = _valTechHomePage.OpenContactUsPage();
            }
            else
            {
                if (_valTechHomePage.PageMenu.Displayed.Equals(true))
                {
                    _valTechHomePage.PageMenu.Click();
                    _wrapperComponent = _valTechHomePage.OpenWrapperComponent();
                    switch (pageName.ToLower())
                    {
                        case ValTechHomePage.Work:
                            _workPage = _wrapperComponent.OpenWorkpage();
                            break;

                        case ValTechHomePage.About:
                            _aboutPage = _wrapperComponent.OpenAboutPage();
                            break;

                        case ValTechHomePage.Services:
                            _servicesPage = _wrapperComponent.OpenServicesPage();
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("Unknown page");
                    }
                }
                else 
                {
                    //This is for when the page menu button is not displayed and 
                    //page menu are across the top of the page
                    //It has not been tested yet
                    switch (pageName.ToLower())
                    {
                        case ValTechHomePage.Work:
                            _workPage = _valTechHomePage.OpenWorkpage();
                            break;

                        case ValTechHomePage.About:
                            _aboutPage = _valTechHomePage.OpenAboutPage();
                            break;

                        case ValTechHomePage.Services:
                            _servicesPage = _valTechHomePage.OpenServicesPage();
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("Unknown page");
                    }
                }
            }
        }

        [Then(@"the recent blogs section is displayed")]
        public void ThenTheRecentBlogsSectionIsDisplayed()
        {
            _valTechHomePage.SectionHeadersHolder.FindElement(By.XPath($"//h2[text()='{ValTechHomePage.RecentBlogs}']")).Displayed.ShouldBeTrue();
        }

        [Then(@"the page name (.*) is displayed")]
        public void ThenThePageNameAboutIsDisplayed(string pageName)
        {
            switch (pageName.ToLower())
            {
                case ValTechHomePage.Work:
                    _workPage.PageHeader.GetAttribute("textContent").Contains(pageName).ShouldBeTrue();
                    break;

                case ValTechHomePage.About:
                    _aboutPage.PageHeader.GetAttribute("textContent").Contains(pageName).ShouldBeTrue();
                    break;

                case ValTechHomePage.Services:
                    _servicesPage.PageHeader.GetAttribute("textContent").Contains(pageName).ShouldBeTrue();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Unknown page");
            }
        }

        [Then(@"the number offices is shown in the output display")]
        public void ThenTheNumberOfficesIsShownInTheOutputDisplay()
        {
            var numberOfOffices = _contactUsPage.OfficeList.Count;
            Debug.WriteLine($"Number of Valtech Offices is {numberOfOffices}");
        }

        [AfterScenario]
        public void CloseWebDriver()
        {
            _driver.Dispose();
        }
    }
}
