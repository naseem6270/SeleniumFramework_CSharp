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
    class HomePage
    {
        CommonFunctions webControls;
        ExtentTest extentTest;
        public HomePage(CommonFunctions commonFunctions)
        {
            webControls = commonFunctions;
        }

        private By tabSearchByVehicle = By.Id("vehicle");
        private By dropdownYearByVehicle = By.XPath("//div[@id='vehicle-tab']//select[@id='ddlYear']");
        private By dropdownMakeByVehicle = By.Id("ddlMake");
        private By dropdownModelByVehicle = By.Id("ddlModel");
        private By dropdownTrimByVehicle = By.Id("ddlOption");
        private By txtfieldZipCodeByVehicle = By.Id("txtZipCodeByVehicle");
        private By btnFindTiresByVehicle = By.Id("btnFindTiresByVehicle");

        private By tabSearchByItem = By.Id("item");
        private By txtFieldItemNo = By.Id("txtSKU");
        private By txtFieldZipCodeByItem = By.Id("txtZipCodeByItemPart");
        private By btnFindTireLicenseNo = By.Id("btnFindSku");

        public void launchURL(string url)
        {
            webControls.LaunchURL(url);
        }

        public void SearchByVehicle(string year, string make, string model, string trim, string zipcode)
        {
            webControls.DropDownSelection(dropdownYearByVehicle, "Year drop down", year, "Selecting " + year + " from the Year drop down");
            webControls.DropDownSelection(dropdownMakeByVehicle, "Make drop down", make, "Selecting " + make + " from the Make drop down");
            webControls.DropDownSelection(dropdownModelByVehicle, "Model drop down", model, "Selecting " + model + " from the Model drop down");
            webControls.DropDownSelection(dropdownTrimByVehicle, "Trim drop down", trim, "Selecting " + trim + " from the Trim drop down");
            webControls.EnterText(txtfieldZipCodeByVehicle, "Zip Code field", zipcode, "Entering Zipcode as " + zipcode);
            webControls.Click(btnFindTiresByVehicle, "Find Tires button", "Clicking on Find Tires button");

        }

        public void SearchByItem(string itemNo, string zipcode)
        {
            webControls.Click(tabSearchByItem, "Search by Item Tab", "Clicking on Search by Item Tab");
            webControls.EnterText(txtFieldItemNo, "Item No text field", itemNo, "Entering value as " + itemNo + " in the Item no text field");
            webControls.EnterText(txtFieldZipCodeByItem, "Zip code text field", zipcode, "Entering value as " + zipcode + " in the Item no text field");
            webControls.Click(btnFindTireLicenseNo, "Find Tires button", "Clicking on Find Tires button");


        }

    }
}
