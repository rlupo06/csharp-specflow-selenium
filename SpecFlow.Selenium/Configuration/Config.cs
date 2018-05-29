using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SpecFlow.Selenium.Configuration
{
	public class Config
	{
		public readonly String url;
		public readonly DesiredCapabilities desiredCapabilities;

		public Config()
		{
			this.url = Environment.GetEnvironmentVariable("seleniumGrid");
			String deviceName = Environment.GetEnvironmentVariable("deviceName");


			if (url == null)
			{

				Environment.SetEnvironmentVariable("seleniumGrid", "http://localhost:4444/wd/hub");
				this.url = Environment.GetEnvironmentVariable("seleniumGrid");
			}

			if (deviceName == null)
			{
				Environment.SetEnvironmentVariable("deviceName", "chrome");
				deviceName = Environment.GetEnvironmentVariable("deviceName");
			}

			this.desiredCapabilities = getCapabilities(deviceName);
		}

		public DesiredCapabilities getCapabilities(String deviceName)
		{
			String device = getDevice(deviceName);
			Dictionary<string, Object> deviceDictionary = convertStringToDictionary(device);

			if (deviceDictionary.ContainsKey("chromeOptions"))
			{
				ChromeOptions chromeOptions = getChromeOptions(deviceDictionary);

				return chromeOptions.ToCapabilities() as DesiredCapabilities;
			}
			else
			{
				return new DesiredCapabilities(deviceDictionary);
			}
		}

		public ChromeOptions getChromeOptions(Dictionary<string, object> desiredCapabilities)
		{
			JObject chromeOption = (JObject)desiredCapabilities["chromeOptions"];

			JToken chromeOptionWindow = chromeOption["args"];

			List<string> chromeOptions = JsonConvert.DeserializeObject<List<string>>(chromeOptionWindow.ToString());

			ChromeOptions options = new ChromeOptions();

			options.AddArguments(chromeOptions);

			return options;
		}

		private String getDevice(String deviceName)
		{

			String json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Devices/Devices.json"));

			JObject devices = JObject.Parse(json);

			return devices[deviceName].ToString();
		}


		private Dictionary<String, Object> convertStringToDictionary(String device)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, object>>(device);
		}


	}
}