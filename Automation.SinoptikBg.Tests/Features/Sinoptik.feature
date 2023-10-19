Feature: Sinoptik
    As a Sinoptik user
	I would like to be able to choose different cities and forecasts
	So that I can be sure the forecast is for the correct city and with correct forecast days

Background:
	Given Sinoptik main page has been opened

Scenario: The selected city forecast is correct
    When type birth city and country 'Варна, България' and select it from dropdown
	Then 'Варна, България' forecast is loaded

Scenario: The selected ten days forecast is correct
		And type birth city and country 'Варна, България' and select it from dropdown
	When select 10 days forecast
	Then the correct days of week and dates are displayed

