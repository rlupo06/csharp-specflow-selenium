using System;
using OpenQA.Selenium.Remote;

namespace SpecFlow.Selenium.Driver
{
	public class DriverFactory
	{

		public readonly Uri url;
		public readonly DesiredCapabilities desiredCapabilities;


		public DriverFactory(String url, DesiredCapabilities desiredCapabilities)
		{
			this.url = new Uri(url);
			this.desiredCapabilities = desiredCapabilities;
		}

		public RemoteWebDriver CreateDriver()
		{
			return new RemoteWebDriver(url, desiredCapabilities);
		}
	}
}
