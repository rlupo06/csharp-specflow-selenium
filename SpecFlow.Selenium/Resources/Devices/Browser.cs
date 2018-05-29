using System;
using System.Collections.Generic;

namespace SpecFlow.Selenium.Resources.Devices
{
    public class Browser
    {
		public string browserName { get; set; }
        public ChromeOptions chromeOptions { get; set; }

		public class ChromeOptions
        {
            public List<string> args { get; set; }
        } 
    }
}
