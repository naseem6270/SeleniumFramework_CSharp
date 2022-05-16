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
	class TestSearchByVehicle : BaseClass
	{
		string url,	year, make, model, trim, zipcode, tireSize;
		

		public TestSearchByVehicle()
			{
			var settings = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("Configurations\\TestData.json").Build();
			year = settings["Year"];
			make = settings["Make"];
			model = settings["Model"];
			trim = settings["Trim"];
			zipcode = settings["ZipCode"];
			tireSize = settings["TireSize"];
			settings = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("Configurations\\config.json").Build();
			url = settings["URL"];
		}

		[Test]
		public void SearchByVehicle()
		{
			
			HomePage homePage = new HomePage(webControls);
			SelectTireCenter selectTireCenter = new SelectTireCenter(webControls);
			SizeSelection sizeSelection = new SizeSelection(webControls);
			TireResult tireResult = new TireResult(webControls);
			homePage.launchURL(url);
			homePage.SearchByVehicle(year, make, model, trim, zipcode);
			selectTireCenter.SelectFirstTireCenter();
			sizeSelection.SelectingDifferentTiresSize(tireSize);
			sizeSelection.ContinueToViewTires();
			tireResult.ValidatingResultCount();
		}


	}
}
