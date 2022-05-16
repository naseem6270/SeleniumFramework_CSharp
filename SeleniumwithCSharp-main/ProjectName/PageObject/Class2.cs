using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace iTIUM.TestProject
{
    class Class2
    {

        static void Main(string[] args)
        {

            Debug.WriteLine("Hello User1 "+ Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent);

            string folderPath = DateTime.Now.ToString().Replace("/", "_").Replace(":", "_");

            Trace.WriteLine("Hello User10 " + folderPath);
        }
    }
}
