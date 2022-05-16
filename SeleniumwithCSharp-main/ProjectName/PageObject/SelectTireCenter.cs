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
    class SelectTireCenter
    {
        CommonFunctions webControls;
        ExtentTest extentTest;
        public SelectTireCenter(CommonFunctions commonFunctions)
        {
            webControls = commonFunctions;
        }

        By modalSelectTireCenter            =   By.Id("SearchWarehouseLabel");
        By btnFind                          =   By.Id("findgeoCode");
        By btnFirstSelectTireCenter         =   By.XPath("(//div[@id='dialog-confirm-SearchWarehouse']//button[contains(@class,'btnSelectWarehouse')])[1]");
              
     
         public void SelectFirstTireCenter()
        {
            webControls.WaitForElementToBeVisible(modalSelectTireCenter, 60);
            webControls.Click(btnFind, "Find Button", "Clicking on Find Button in the Select Tire Center Modal");
            webControls.Click(btnFirstSelectTireCenter, "Select Tire Center Button", "Clicking on the first Select Tire Center button");

        }
        
    }
}
