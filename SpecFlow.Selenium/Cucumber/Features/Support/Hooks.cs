using System;
using TechTalk.SpecFlow;
using BoDi;
using OpenQA.Selenium;
using SpecFlow.Selenium.Driver;
using SpecFlow.Selenium.Configuration;

namespace SpecFlow.Selenium.Features.Support
{
	[Binding]
	public sealed class Hooks
	{

		private readonly IObjectContainer objectCContainer;
		private IWebDriver driver;
		private static DriverFactory driverFactory;
		private static String applicationUrl = "https://www.google.com/";

		public Hooks(IObjectContainer objectContainer)
		{
			this.objectCContainer = objectContainer;
		}

		[BeforeTestRun]
		public static void BeforeTestRun()
		{

			Config config = new Config();

			driverFactory = new DriverFactory(config.url, config.desiredCapabilities);
		}

		[BeforeScenario]
		public void BeforeScenario()
		{
			this.driver = driverFactory.CreateDriver();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
			driver.Navigate().GoToUrl(applicationUrl);

			objectCContainer.RegisterInstanceAs(driver);

		}

		[AfterScenario]
		public void AfterScenario()
		{
			if (driver != null)
			{
				driver.Dispose();
			}
		}
	}
}

