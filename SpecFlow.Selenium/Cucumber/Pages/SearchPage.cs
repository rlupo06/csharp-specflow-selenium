using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SpecFlow.Selenium.Pages
{
	public class SearchPage : PageObject
    
	{
		[FindsBy(How = How.CssSelector, Using = "input[value='Google Search']")]
        private IWebElement searchButton;

        [FindsBy(How = How.CssSelector, Using = "#lst-ib")]
        private IWebElement searchBox;

        [FindsBy(How = How.CssSelector, Using = "#ires .g .r a")]
        private IList<IWebElement> searchResults;
        
		public SearchPage(IWebDriver driver) : base(driver)
		{
			Trait();
		}

		public override void Trait()
        {
			Assert.IsTrue(searchButton.Displayed);
        }

		public void PerformSearch(string searchText)
		{
			searchBox.Clear();
			searchBox.SendKeys(searchText);
			searchBox.Submit();
		}

		public void SelectResult(string expResult)
		{
			IWebElement link = FindResult(expResult);
			Assert.IsNotNull(link, $"Could not find link for: {expResult}");
			link.Click();
		}

		private IWebElement FindResult(string expResult)
		{
			foreach (IWebElement link in searchResults)
			{
				if (link.Text.ToUpper().Contains(expResult.ToUpper()))
				{
					return link;
				}
			}
			return null;
		}      
	}
}