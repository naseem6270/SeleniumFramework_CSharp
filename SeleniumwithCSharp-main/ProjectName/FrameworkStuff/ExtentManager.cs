using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace iTIUM.FrameworkStuff
{
    public class ExtentManager
    {
        public static ExtentHtmlReporter htmlReporter;
        public static ExtentReports extent;
        
        private ExtentManager()
        {
            
        }

        public static ExtentReports GetInstance()
        {
            if (extent == null)
            {
                string projectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.ToString(); //Get Current Project Path
                string folderName = CommonFunctions.setUniqueKey(); //Generate Random folder name
                string reportingFolderPath = projectPath+"\\Reporting\\Report_"+ folderName+"\\"; 
                System.IO.Directory.CreateDirectory(reportingFolderPath); //Creating Reporting directory
                htmlReporter = new ExtentHtmlReporter(reportingFolderPath);
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
                extent.AddSystemInfo("OS", "Windows");
                extent.AddSystemInfo("Environment", "QA");
                extent.AddSystemInfo("Browser", "Chrome");
                string extentConfigPath = projectPath+"\\"+"extent-config.xml";
                htmlReporter.LoadConfig(extentConfigPath);
            }

            return extent;
        }


    }


}
