using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SpecFlow.Selenium.Resources.Devices;

namespace SpecFlow.Selenium.Configuration
{
	public class Config
	{
		public readonly String url;
		public readonly DesiredCapabilities desiredCapabilities;
		public Config()
		{
			this.url = Environment.GetEnvironmentVariable("seleniumGrid");
			if (url == null)
			{

				Environment.SetEnvironmentVariable("seleniumGrid", "http://localhost:4444/wd/hub");
				this.url = Environment.GetEnvironmentVariable("seleniumGrid");
			}

			String deviceName = Environment.GetEnvironmentVariable("deviceName");
			if (deviceName == null)
			{
				Environment.SetEnvironmentVariable("deviceName", "chrome");
				deviceName = Environment.GetEnvironmentVariable("deviceName");
			}

			this.desiredCapabilities = getDevice(deviceName);
		}
        
		private DesiredCapabilities getDevice(String deviceName)
		{

			//DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
            //DesiredCapabilities capabilities = new DesiredCapabilities();
            //capabilities.setCapability("BrowserName", "chrome");
            //ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--window-size=1920,1080");
            //options.

            //return new RemoteWebDriver(url, options);



			String json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Devices/Devices.json"));

			JObject devices = JObject.Parse(json);
            
			JToken device = devices[deviceName];

			Dictionary<string, object> chromeOptions = JsonConvert.DeserializeObject<Dictionary<string, object>>(device.ToString());

			JObject chromeOption =(JObject)chromeOptions["chromeOptions"];

			JToken chromeOptionWindow = chromeOption["args"];

			List<string> videogames = JsonConvert.DeserializeObject<List<string>>(chromeOptionWindow.ToString());


			ChromeOptions options = new ChromeOptions();

			options.AddArguments(videogames);

			//options(htmlAttributes);


			//ChromeOptions chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("test-type");
            //capabilities = (DesiredCapabilities)chromeOptions.ToCapabilities();

            //new RemoteWebDriver(hubUri, capabilities, TimeSpan.FromSeconds(180));







			//Browser browser = new Browser();

			//Browser browsers = (Browser)device.ToObject(browser.GetType());




			//if (browsers.chromeOptions.args[0] != null)
			//{

			//	ChromeOptions options = new ChromeOptions();
			//	options.AddArguments(browsers.chromeOptions.args);
			//	desiredCapabilities.SetCapability(ChromeOptions.Capability, options);
			//}
			//desiredCapabilities.SetCapability("browserName", browsers.browserName);
              
			return desiredCapabilities;



		}
	}
}

