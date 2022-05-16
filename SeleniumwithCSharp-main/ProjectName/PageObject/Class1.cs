using System;
using AventStack.ExtentReports;
using iTIUM.FrameworkStuff;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SeleniumFirstProj;

namespace iTIUM.TestProject
{
	class Class1 : BaseClass
	{
		string url;
		ExtentReports rep = ExtentManager.GetInstance();
		ExtentTest extentTest;

		public Class1()
			{
			var settings = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("Configurations\\config.json").Build();
			url = settings["URL"];
		}

		[Test]
		public void MyFirstTest()
		{
			extentTest = rep.CreateTest("My First Test");
			webControls.SetReporter(extentTest);
			webControls.LaunchURL(url);
			
			extentTest.Log(Status.Pass, "Application is properly showing the Home Page");
			extentTest.Fail("Failed due to XXX", MediaEntityBuilder.CreateScreenCaptureFromPath("D:\\CSharpWS\\Reports\\Screenshot_1.png").Build());


			// test with snapshot
			//extentTest.AddScreenCaptureFromPath("Screenshot_11.png");
			
			
			
		}

	}
}
