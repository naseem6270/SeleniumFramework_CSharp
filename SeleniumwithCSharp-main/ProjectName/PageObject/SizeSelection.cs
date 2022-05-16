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
    class SizeSelection
    {
        CommonFunctions webControls;

        public SizeSelection(CommonFunctions commonFunctions)
        {
            webControls = commonFunctions;
        }

        By checkBoxVerified = By.Id("chkVerifiedTheSizeSelection");
        By btnContinueViewTires = By.Id("btnContinueToViewTires");


        public void SelectingDifferentTiresSize(string TireSize)
        {
            By radioBtnInchSize = By.XPath("(//input[@data-size='" + TireSize + "'])[1]");
            webControls.WaitForElementToBePresent(checkBoxVerified, 60);
            webControls.ClickUsingJavaScriptExecutor(radioBtnInchSize, TireSize + " Inch Radio Button", "Clicking on " + TireSize + " Inch Radio Button in the Size Selection Page");
        }

        public void ContinueToViewTires()
        {
            webControls.WaitForElementToBePresent(checkBoxVerified, 60);
            webControls.ClickUsingJavaScriptExecutor(checkBoxVerified, "Checkbox Verified", "Clicking on the Verified Checkbox");
            webControls.Click(btnContinueViewTires, "Continue to View Tires button", "Clicking on Continue to View Tires button");

        }
    }


}
