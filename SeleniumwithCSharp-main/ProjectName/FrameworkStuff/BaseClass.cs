using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using iTIUM.FrameworkStuff;
using System.Diagnostics;

namespace SeleniumFirstProj
{
	class BaseClass
	{
		public IWebDriver driver;
		public CommonFunctions webControls;
		string browserName;
		ExtentReports extentRep = ExtentManager.GetInstance();
		ExtentTest extentTest;
		//ExtentTest extentTest;

		public BaseClass()
		{
			var settings = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("Configurations\\config.json").Build();
			browserName = settings["BrowserType"];
			string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
			string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
			string projectPath = new Uri(actualPath).LocalPath;
		}

		public IWebDriver GetDriver()
		{
			if (driver == null)
			{
				CreateDriver();
				return driver;
			}
			else
			{
				return driver;
			}

		}

		[SetUp]
		public void BeforeMethod()
		{
			CreateDriver();
			var currentTestName = TestContext.CurrentContext.Test.Name;
			extentTest = extentRep.CreateTest(currentTestName);
			webControls.SetReporter(extentTest); 
			 
		}

			public void CreateDriver()
		{
			switch (browserName.ToLower()) {
				case "firefox":
					driver = new FirefoxDriver();
					break;
				case "chrome":
					ChromeOptions options = new ChromeOptions();
					options.AddArguments("test-type");
					options.AddArguments("start-maximized");
					options.AddArguments("--window-size=1920,1080");
					options.AddArguments("--enable-precise-memory-info");
					options.AddArguments("--disable-popup-blocking");
					options.AddArguments("--disable-default-apps");
					options.AddArguments("test-type=browser");
					options.AddArgument("--ignore-certificate-errors");
					options.AddArgument("--incognito");

					driver = new ChromeDriver(options);

					break;
				case "internet explorer":
					driver = new InternetExplorerDriver();
					break;
				case "edge":
					driver = new EdgeDriver();
					break;
			}
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
			webControls = new CommonFunctions();
			webControls.SetDriver(driver);
			
		}
				

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
			extentRep.Flush();
		}

		[OneTimeTearDown]
		public void EndReport()

		{
			//End Report
			extentRep.Flush();

		}
	}
}
