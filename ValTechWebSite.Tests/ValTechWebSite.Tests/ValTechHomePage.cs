using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace ValTechWebSite.Tests
{
    [Binding]
    public class ValTechHomePage
    {
        public const string RecentBlogs = "recent blogs",
                            Work = "work",
                            Services = "services",
                            About = "about",
                            ContactUs = "Contact Us";

        private readonly IWebDriver _driver;
        private const string HomePage = @"http://www.valtech.co.uk";

        [FindsBy(How = How.CssSelector, Using = "h2.block-header__heading")]
        public IWebElement SectionHeadersHolder { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul.header__navigation__menu.navigation__menu")]
        public IWebElement HeaderNavigationMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".navigation__menu__item")]
        public IList<IWebElement> NavigationMenuList { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".hamburger__front")]
        public IWebElement PageMenu { get; set; }

        [FindsBy(How = How.Id, Using = "CybotCookiebotDialogBodyButtonAccept")]
        public IWebElement AcceptCookiesButton { get; set; }

        public ValTechHomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public static ValTechHomePage NavigateToHomePage(IWebDriver driver)
        {
            //Tests work in debug but not in run mode 
            //Adding thread sleeps so that the pages are loaded and elements are visible, definitely not ideal
            driver.Navigate().GoToUrl(HomePage);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Thread.Sleep(5000);
            return new ValTechHomePage(driver);
        }

        public WorkPage OpenWorkpage()
        {
            HeaderNavigationMenu.FindElement(By.PartialLinkText(Work)).Click();
            Thread.Sleep(5000);
            return new WorkPage(_driver);
        }

        public ServicesPage OpenServicesPage()
        {
            HeaderNavigationMenu.FindElement(By.PartialLinkText(Services)).Click();
            Thread.Sleep(5000);
            return new ServicesPage(_driver);
        }

        public AboutPage OpenAboutPage()
        {
            HeaderNavigationMenu.FindElement(By.PartialLinkText(About)).Click();
            Thread.Sleep(5000);
            return new AboutPage(_driver);
        }

        public ContactUsPage OpenContactUsPage()
        {
            _driver.Navigate().GoToUrl(ContactUsPage.ContactUsPageUri);
            Thread.Sleep(5000);
            return new ContactUsPage(_driver);
        }

        public WrapperComponent OpenWrapperComponent()
        {
            return new WrapperComponent(_driver);
        }

        public void AcceptCookies()
        {
            if (AcceptCookiesButton.Displayed)
            {
                AcceptCookiesButton.Click();
            }
            Thread.Sleep(2000);
        }
    }

    //All these classes should be in their own individual files

    [Binding]
    public class WorkPage
    {
        public const string WorkPageUri = @"http://www.valtech.co.uk/work/";
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = ".page-header h1")]
        public IWebElement PageHeader { get; set; }

        public WorkPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }

    [Binding]
    public class ServicesPage
    {
        public const string ServicesPageUri = @"http://www.valtech.co.uk/services/";
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = ".page-header h1")]
        public IWebElement PageHeader { get; set; }

        public ServicesPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }

    [Binding]
    public class AboutPage
    {
        public const string AboutPageUri = @"http://www.valtech.co.uk/about/";
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = ".page-header h1")]
        public IWebElement PageHeader { get; set; }

        public AboutPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }

    [Binding]
    public class ContactUsPage
    {
        public const string ContactUsPageUri = @"http://www.valtech.co.uk/about/contact-us/";
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "h2.office__heading")]
        public IList<IWebElement> OfficeList { get; set; }

        public ContactUsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }

    [Binding]
    public class WrapperComponent
    {
        public const string RecentBlogs = "recent blogs",
                            Work = "work",
                            Services = "services",
                            About = "about";

        public const string WrapperUri = @"http://www.valtech.co.uk/";
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "#navigationMenuWrapper .navigation__menu")]
        public IWebElement HeaderNavigationMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".navigation__menu__item")]
        public IList<IWebElement> NavigationMenuItemList { get; set; }

        public WrapperComponent(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public AboutPage OpenAboutPage()
        {
            HeaderNavigationMenu.FindElement(By.XPath($".//a[contains(@href, '{About}')]")).Click();
            Thread.Sleep(2000);
            return new AboutPage(_driver);
        }

        public WorkPage OpenWorkpage()
        {
            HeaderNavigationMenu.FindElement(By.XPath($".//a[contains(@href, '{Work}')]")).Click();
            Thread.Sleep(2000);
            return new WorkPage(_driver);
        }

        public ServicesPage OpenServicesPage()
        {
            HeaderNavigationMenu.FindElement(By.XPath($".//a[contains(@href, '{Services}')]")).Click();
            Thread.Sleep(2000);
            return new ServicesPage(_driver);
        }
    }
}
