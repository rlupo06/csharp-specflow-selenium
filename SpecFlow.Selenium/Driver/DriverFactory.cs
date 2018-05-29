using System;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SpecFlow.Selenium.Driver
{
    public class DriverFactory
    {
     
		public readonly Uri url;
		public readonly DesiredCapabilities desiredCapabilities;

		public DriverFactory(String url, DesiredCapabilities desiredCapabilties)
        {
			this.url = new Uri(url);
			this.desiredCapabilities = desiredCapabilties;
        }

		public RemoteWebDriver createDriver(){

			String json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Devices/Devices.json"));

            JObject devices = JObject.Parse(json);

			JToken device = devices["414px"];

            Dictionary<string, object> chromeOptions = JsonConvert.DeserializeObject<Dictionary<string, object>>(device.ToString());

			if (chromeOptions["browserName"].Equals("chrome"))
			{

				JObject chromeOption = (JObject)chromeOptions["chromeOptions"];

				JToken chromeOptionWindow = chromeOption["args"];

				List<string> videogames = JsonConvert.DeserializeObject<List<string>>(chromeOptionWindow.ToString());


				ChromeOptions options = new ChromeOptions();

				options.AddArguments(videogames);

				return new RemoteWebDriver(url, options);
			}
			else
			{
				DesiredCapabilities desiredCapabilities = new DesiredCapabilities(chromeOptions);
				return new RemoteWebDriver(url, desiredCapabilities);
			}
			//return new RemoteWebDriver(url, desiredCapabilities);

		}
    }
}
