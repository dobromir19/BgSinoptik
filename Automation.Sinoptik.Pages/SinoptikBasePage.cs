using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Automation.Sinoptik.Pages
{
    public class SinoptikBasePage
    {
        private IWebDriver driver;

        public SinoptikBasePage()
        {
            //if we want to disable notification and maximize window
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            options.AddArgument("--start-maximized");
            this.driver = new ChromeDriver(options);

            //driver = new ChromeDriver();
        }

        private IWebElement NetInfoPopUp => driver.FindElement(By.ClassName("didomi-popup-container.didomi-popup__dialog didomi-popup-notice.didomi-popup-notice-info-type.didomi-popup-notice-with-data-processing"));
        private IWebElement City => driver.FindElement(By.Id("searchField"));
        private IWebElement FromDropdown => driver.FindElements(By.CssSelector("div[data-baseweb='select']"))[0];
        private IWebElement CityPageForecast => driver.FindElements(By.CssSelector("div[data-baseweb='select']"))[0];
        private IWebElement AcceptAndCloseButton => driver.FindElement(By.Id("didomi-notice-agree-button"));
        private IWebElement CloseAddButton => driver.FindElement(By.CssSelector(".ad-TransitionClose"));
        private IWebElement InputCityField => driver.FindElement(By.Id("searchField"));
        private IWebElement SearchButton => driver.FindElement(By.CssSelector("searchTopButton"));
        private IList<IWebElement> CitiesDropDown => driver.FindElements(By.CssSelector(".autocomplete"));
        private IWebElement FirstFoundResult => driver.FindElement(By.CssSelector(".autocomplete > a"));
        private IWebElement CurrentCity => driver.FindElement(By.CssSelector(".currentCityHeading > h1"));
        private IWebElement CurrentCountry => driver.FindElement(By.CssSelector(".currentCityInfo a:nth-of-type(2)"));
        private IWebElement TenDaysForecastButton => driver.FindElement(By.CssSelector(".wf10day"));
        private IList<IWebElement> ForecastBox => driver.FindElements(By.CssSelector(".wf10dayRight"));
        private IWebElement Header => driver.FindElement(By.CssSelector(".headerMain"));

        public void AcceptAndCloseNetInfo()
        {
            AcceptAndCloseButton.Click();
        }

        public void CloseAdd()
        {
            WaitUntilElementIsVisible(By.CssSelector(".ad-TransitionClose"));
            CloseAddButton.Click();
        }

        public void OpenSinoptikBg()
        {      
            driver.Navigate().GoToUrl("https://www.sinoptik.bg/");
            WaitUntilElementIsVisible(By.CssSelector(".didomi-popup-container.didomi-popup__dialog.didomi-popup-notice.didomi-popup-notice-info-type.didomi-popup-notice-with-data-processing"));
            AcceptAndCloseNetInfo();

            CloseAdd();
            WaitForLoad();
        }

        public void SelectFirstMatchingResult()
        {
           WaitUntilElementIsVisible(By.CssSelector(".autocomplete"));
           FirstFoundResult.Click();
        }

        public void WaitUntilElementIsVisible(By element)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
        }

        public void TypeAndSelectCity(string cityNameAndCountryName)
        {
            InputCityField.Click();
            InputCityField.SendKeys(cityNameAndCountryName);
            SelectFirstMatchingResult();
            WaitForLoad();
        }

        public string GetCurrentCity()
        {
            string city = CurrentCity.Text;
            return city;
        }

        public string GetCurrentCountry()
        {
            string country = CurrentCountry.Text;
            return country;
        }

        public void Select10DaysForecast()
        {
            TenDaysForecastButton.Click();
            WaitForLoad();
        }

        public List<string> GetDays()
        {
            List<string> actualDays = new List<string>();

            foreach (var item in ForecastBox)
            {
                var day = item.FindElement(By.CssSelector(".wf10dayRightDay")).Text;
                actualDays.Add(day);
            }

            return actualDays;
        }

        public List<string> GetDates()
        {
            List<string> actualDates = new List<string>();

            foreach (var item in ForecastBox)
            {
                var date = item.FindElement(By.CssSelector(".wf10dayRightDate")).Text;
                actualDates.Add(date);
            }

            return actualDates;
        }

        public virtual void WaitForLoad()
        {
            WaitUntilElementIsVisible(By.CssSelector(".headerMain"));
        }

        public void QuitDriver()
        {
            driver.Quit();
        }
    }
}

