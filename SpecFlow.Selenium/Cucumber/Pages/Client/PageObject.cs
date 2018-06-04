using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

namespace SpecFlow.Selenium.Pages
{
	public abstract class PageObject
	{

		private readonly IWebDriver driver;

		public PageObject(IWebDriver driver)
		{

			this.driver = driver;

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			RetryingElementLocator retry = new RetryingElementLocator(driver, TimeSpan.FromSeconds(15));
			IPageObjectMemberDecorator decor = new DefaultPageObjectMemberDecorator();
			PageFactory.InitElements(retry.SearchContext, this, decor);
		}

		public abstract void Trait();

		public void CheckUrl(string expectedUrl)
		{
			string actualUrl = driver.Url;
			Assert.AreEqual(expectedUrl, actualUrl);
		}
              
		public void WaitForAjax()
		{
			try
			{
				WaitForAjaxToStart();
				WaitForAjaxToFinish();
			}
			catch (WebDriverException e)
			{
                //ajax never started or never finished. Add this info to a logger
			}
		}

		private void WaitForAjaxToStart()
        {
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => !(Boolean)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));

        }

		private void WaitForAjaxToFinish()
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
			wait.Until(driver => (Boolean)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));
		}      
	}
}