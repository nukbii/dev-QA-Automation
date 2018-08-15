using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace ValTechWebSite.Tests
{
    public class ValTechHomePage
    {
        private readonly IWebDriver _driver;
        private const string HomePage = @"http://www.valtech.com";

        [FindsBy(How = How.CssSelector, Using = "div.banner-slider__slide-content__intro")]
        public IWebElement LatestNews { get; set; }

        public ValTechHomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public static ValTechHomePage NavigateToHomePage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(HomePage);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return new ValTechHomePage(driver);
        }
    }
}
