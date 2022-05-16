using System;
using System.Threading;
using AventStack.ExtentReports;
using iTIUM.FrameworkStuff;
using iTIUM.PageObject;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SeleniumFirstProj;

namespace iTIUM.TestProject
{
	class TestSearchByItem : BaseClass
	{
		string url,	itemNo, zipCode;
		
		public TestSearchByItem()
			{
			var settings = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("Configurations\\TestData.json").Build();
			itemNo = settings["ItemNo"];
			zipCode = settings["ZipCode"];
			settings = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("Configurations\\config.json").Build();
			url = settings["URL"];
		}

		[Test]
		public void SearchByItem()
		{
			

			HomePage homePage = new HomePage(webControls);
			SelectTireCenter selectTireCenter = new SelectTireCenter(webControls);
			TireResult tireResult = new TireResult(webControls);

			homePage.launchURL(url);
			homePage.SearchByItem(itemNo, zipCode);
			selectTireCenter.SelectFirstTireCenter();
			tireResult.validateItemNoResultPage(itemNo);
			
		}


	}
}
