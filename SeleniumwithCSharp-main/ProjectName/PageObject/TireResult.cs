using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace iTIUM.PageObject
{
    class TireResult
    {
        CommonFunctions webControls;
        ExtentTest extentTest;

        public TireResult(CommonFunctions commonFunctions)
        {
            webControls = commonFunctions;
        }

        By sectionResultsSearchByVehicle = By.XPath("//div[@class='vehicle-results']");
        By txtResultCount = By.XPath("//div[contains(@class,'items-count')]");
        By txtItemNo = By.XPath("//div[@class='result_item_id']//span[@class='itemSpecValue']");


        public void WaitingForResultPage()
        {
            webControls.WaitForElementToBeVisible(sectionResultsSearchByVehicle, 90);
        }

        public void ValidatingResultCount()
        {
            webControls.WaitForElementToBeVisible(txtResultCount, 60);
            extentTest = webControls.GetReporter();

            if (webControls.IsElementPresent(txtResultCount, "Results Count"))
            {
                extentTest.Pass("Application is properly showing the Result Count Text as " + webControls.GetText(txtResultCount, "Result Count"));

            }
            else
            {
                extentTest.Fail("Application is NOT properly showing the Result Count Text as " + webControls.GetText(txtResultCount, "Result Count"), MediaEntityBuilder.CreateScreenCaptureFromPath(webControls.TakeScreenshot()).Build());

            }
        }

        public void validateItemNoResultPage(string itemNo)
        {
            webControls.WaitForElementToBeVisible(txtItemNo, 60);
            webControls.CompareString(webControls.GetText(txtItemNo, "Item No text"), itemNo, "Application is properly showing the Item no in the Result page", " Application is NOT properly showing the Item no in the Result page");

        }
    }


}



