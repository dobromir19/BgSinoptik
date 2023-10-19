using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Automation.Sinoptik.Pages;
using System.Globalization;
using System.Collections.Generic;

namespace Automation.Sinoptik.Tests.Steps
{
    [Binding]
    public class SinoptikSteps
    {
        private SinoptikBasePage sinoptikBasePage;

        public SinoptikSteps(SinoptikBasePage sinoptikBasePage)
        {
            this.sinoptikBasePage = sinoptikBasePage;
        }

        [AfterScenario]
        public void After()
        {
            sinoptikBasePage.QuitDriver();
        }

        [StepDefinition(@"Sinoptik main page has been opened")]
        public void TheMainPageIsOpened()
        {
            sinoptikBasePage.OpenSinoptikBg();
        }

        [StepDefinition(@"type birth city and country '(.*)' and select it from dropdown")]
        public void TheCityIsSelected(string cityAndCountry)
        {
            sinoptikBasePage.TypeAndSelectCity(cityAndCountry);
        }

        [StepDefinition(@"'(.*)' forecast is loaded")]
        public void VerifyTheCorrectCityCountryForecastIsLoaded(string cityAndCountry)
        {
            string actualCountry = sinoptikBasePage.GetCurrentCountry();
            string expectedCountry = cityAndCountry.Substring(cityAndCountry.IndexOf(",") + 2);

            string actualCity = sinoptikBasePage.GetCurrentCity();
            string expectedCity = cityAndCountry.Substring(0, cityAndCountry.IndexOf(","));

            Assert.AreEqual(expectedCity, actualCity, "The selected city forecast is not correct.");
            Assert.AreEqual(expectedCountry, actualCountry, "The selected country forecast is not correct.");
        } 
        
        [StepDefinition(@"select 10 days forecast")]
        public void The10DaysForecastIsSelected()
        {
            sinoptikBasePage.Select10DaysForecast();
        }

        [StepDefinition(@"the correct days of week and dates are displayed")]
        public void VerifyTheCorectatesAndDatesAreDisplayed()
        {
            List<string> arrayDays =new List<string>();
            List<string> arrayDates =new List<string>();

            for (int i = 0; i < 10; i++)
            {
                DateTime currentDateTime = DateTime.UtcNow.AddDays(i);
                string formattedDates = currentDateTime.ToString("dd.MM.yy", new CultureInfo("bg"));
                arrayDates.Add(formattedDates);   
                string formattedDays = currentDateTime.ToString("ddd", new CultureInfo("bg"));

                switch (formattedDays) 
                {
                    case "пон":
                    formattedDays = "Пн.";
                    break;
                    case "вт":
                    formattedDays = "Вт.";
                    break;
                    case "ср":
                    formattedDays = "Ср.";
                    break;
                    case "четв":
                    formattedDays = "Чт.";
                    break;
                    case "пет":
                    formattedDays = "Пт.";
                    break;
                    case "съб":
                    formattedDays = "Сб.";
                    break;
                    case "нед":
                    formattedDays = "Нд.";
                    break;
                }
                arrayDays.Add(formattedDays);
            }

            List<string> expectedDays = arrayDays;
            List<string> actualDays = sinoptikBasePage.GetDays();
            List<string> expectedDates = arrayDates;
            List<string> actualDates = sinoptikBasePage.GetDates();

            Assert.AreEqual(expectedDays, actualDays, "The days are not correct");
            Assert.AreEqual(expectedDates, actualDates, "The dates are not correct");   
        }
    }
}

