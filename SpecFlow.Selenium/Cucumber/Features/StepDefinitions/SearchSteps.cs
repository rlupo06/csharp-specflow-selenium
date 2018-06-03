using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using SpecFlow.Selenium.Pages;

namespace SpecFlow.Selenium.Features.StepDefinitions
{
	[Binding]
	public class SearchSteps
	{
		private readonly IWebDriver driver;
		private readonly SearchPage page;

		public SearchSteps(IWebDriver driver)
		{
			this.driver = driver;
			this.page = new SearchPage(this.driver);
		}

		[When(@"I search for ""(.*)""")]
		public void WhenISearchFor(string searchText)
		{
			page.PerformSearch(searchText);
		}

		[When(@"select ""(.*)"" in the search results")]
		public void WhenSelectInTheSearchResults(string expectedLink)
		{
			page.SelectResult(expectedLink);
		}

		[Then(@"I am presented with the ""(.*)"" homepage")]
        public void ThenIAmPresentedWithTheHomepage(string expectedUrl)
        {
            page.CheckUrl(expectedUrl);
        }
	}
}
