using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using AventStack.ExtentReports;
using iTIUM.FrameworkStuff;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumFirstProj;

class CommonFunctions
{
    ExtentTest extentTest;
    string projectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.ToString();
    IWebDriver driver;
    static string uniqueKey;

    public void SetDriver(IWebDriver baseClassDriver)
    {
        driver = baseClassDriver;
    }

    public IWebDriver getDriver()
    {
        return driver;
    }

    public void SetReporter(ExtentTest _extentTest)
    {
        extentTest = _extentTest;
    }

    public ExtentTest GetReporter()
    {
        return extentTest;
    }

    public static string getUniqueKey()
    {
        return uniqueKey;
    }

    public static string setUniqueKey()
    {
        uniqueKey = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").ToString();
        return uniqueKey;

    }

    public void LaunchURL(String url)
    {
        driver.Navigate().GoToUrl(url);
        extentTest.Log(Status.Info, "Launching URL " + url);
    }

    public void Click(By prop, String elemName, string msg)
    {
        WaitForElementToBePresent(prop, elemName);

        try
        {
            WaitForElementToBeClickable(prop, 60);
            driver.FindElement(prop).Click();
            extentTest.Info(msg);
        }
        catch (Exception e)
        {
            extentTest.Fail("Got Exception " + e.Message + " while clicking on element name " + elemName, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            throw e;
        }
    }

    public void ClickUsingJavaScriptExecutor(By prop, String elemName, string msg)
    {
        IWebElement element = driver.FindElement(prop);
        WaitForElementToBePresent(prop, elemName);

        try
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", element);

            extentTest.Info(msg);
        }
        catch (Exception e)
        {
            extentTest.Fail("Got Exception " + e.Message + " while clicking on element name " + elemName, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            throw e;
        }
    }

    public void EnterText(By prop, string elemName, string text, string msg)
    {
        WaitForElementToBePresent(prop, elemName);

        WaitForElementToBeClickable(prop, 60);
        try
        {
            driver.FindElement(prop).Click();
            driver.FindElement(prop).SendKeys(text);
            extentTest.Info(msg);
        }
        catch (Exception e)
        {
            extentTest.Fail("Got Exception " + e.Message + " while entering a value " + text + " from the element " + elemName, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            throw e;
        }

    }

    public void DropDownSelection(By prop, String elemName, string value, string msg)
    {

        WaitForElementToBePresent(prop, elemName);

        WaitForElementToBeClickable(prop, 60);

        try
        {
            IWebElement dropdown = driver.FindElement(prop);
            SelectElement select = new SelectElement(dropdown);
            select.SelectByValue(value);
            extentTest.Info(msg);
        }
        catch (Exception e)
        {
            extentTest.Fail("Got Exception " + e.Message + " while selecting a value " + value + " from the drop down " + elemName, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            throw e;
        }

    }

    public string GetText(By prop, string elemName)
    {
        WaitForElementToBePresent(prop, elemName);

        try
        {
            string text = driver.FindElement(prop).Text;
            extentTest.Info("Getting text from the element " + elemName + " having property as " + prop);
            return text;
        }
        catch (Exception e)
        {
            extentTest.Fail("Got Exception " + e.Message + " while getting a text from the element " + elemName, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            throw e;
        }
    }

    public string GetAttribute(By prop, string attribute, string elemName)
    {

        WaitForElementToBePresent(prop, elemName);

        try
        {
            string text = driver.FindElement(prop).GetAttribute(attribute);
            extentTest.Info("Getting Attribute value " + attribute + " from the element " + elemName + " having property as " + prop, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            return text;
        }
        catch (Exception e)
        {
            extentTest.Fail("Got Exception " + e.Message + " while getting a attribute value " + attribute + " from the element " + elemName);
            throw e;
        }
    }

    private Boolean WaitForElementToBePresent(By prop, String elemName)
    {
        Boolean status = false;
        Exception excep = null;
        resetImplicitWaitToCustomTime(0);
        for (int i = 0; i < 60; i++)
        {
            
            try
            {
                driver.FindElement(prop);
                status = true;
                break;
            }
            catch (NoSuchElementException e)
            {
                Thread.Sleep(1000);
                excep = e;
            }
        }
            if (!status)
            {
                extentTest.Fail("Unable to find an element "+ elemName + " with the property as: " + prop, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            throw excep;
            }

        resetImplicitWaitToDefaultTime();
        return status;

    }


    public Boolean WaitForElementToBePresent(By prop, int timeOut)
    {
        Boolean status = false;
        resetImplicitWaitToCustomTime(0);
        try
        {
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
            driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(prop));
            status = true;
        }
        catch (Exception e)
        {
            extentTest.Fail("Timeout exception while waiting for element " + prop, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            status = false;
            throw e;

        }
        resetImplicitWaitToDefaultTime();

        return status;
    }

    public Boolean WaitForElementToBeVisible(By prop, int timeOut)
    {
        Boolean status = false;
        resetImplicitWaitToCustomTime(0);
        try
        {
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
            driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(prop));
            status = true;
        }
        catch (Exception e)
        {
            extentTest.Fail("Got exception while waiting for element " + prop, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            status = false;
            throw e;

        }
        resetImplicitWaitToDefaultTime();

        return status;
    }

    public Boolean WaitForElementToBeClickable(By prop, int timeOut)
    {
        Boolean status = false;
        resetImplicitWaitToCustomTime(0);

        try
        {
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
            driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(prop));
            status = true;
        }
        catch (Exception e)
        {
            extentTest.Fail("Timeout exception while waiting for element " + prop, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            status = false;
            throw e;

        }
        resetImplicitWaitToDefaultTime();

        return status;
    }

    public bool IsElementPresent(By prop, String elemName)
    {
        Boolean status = false;
        resetImplicitWaitToCustomTime(0);
        try
        {
            driver.FindElement(prop);
            status = true;
        }
        catch (NoSuchElementException e)
        {
            extentTest.Fail("Unable to find any element with the property " + prop, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
            throw e;

        }
       
        resetImplicitWaitToDefaultTime();
        return status;

    }

    public void CompareString(string actual, string expected, String passMsg, String failmsg)
    {
        if (actual.Equals(expected))
        {
            extentTest.Pass(passMsg);

        }
        else
        {

            extentTest.Fail(failmsg + ". Expected was " + expected + " but got from application is " + actual, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot()).Build());
        }


    }

    public string TakeScreenshot()
    {
        System.IO.Directory.CreateDirectory(projectPath + "//Reporting//Report_" + uniqueKey + "//Screenshots"); //Creating Screenshot directory
        string fileName = projectPath + "//Reporting//Report_" + uniqueKey + "//Screenshots//Screenshot" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").ToString() + ".png";
        ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Png);
        return fileName;
    }

    public void resetImplicitWaitToDefaultTime()
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

    }

    public void resetImplicitWaitToCustomTime(int timeout)
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);

    }
}
