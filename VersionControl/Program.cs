using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;



namespace VersionControl
{
    class Program
    {
        /* Takes three or two arguments, file path, file name, and version change.
            If no file path is specified it will assume the file is in the same directory as the application.
         */
        static void Main(string[] args)
        {
            if (args[1].ToLower() != "feature" && args[1].ToLower() != "bugfix")
            {
                Console.WriteLine("Invalid update type, please enter a type of feature or bugfix");
                Console.ReadLine();
            }
            else
            {
                // init the IO and VersionConstroller classes
                IO io = new IO(args[0]);
                string version = io.extractStringFromFile();
                VersionController versionController = new VersionController(version);

                // if an exception was thrown while trying to extract the version number skip to the end
                if (version != "")
                {

                    Console.WriteLine("Current version of {0} is: {1}", io.getFile(), versionController.getVersion());
                    Console.WriteLine("Update type: {0}", args[1]);

                    // split the version numbers from the string
                    versionController.extractVersionNumbers();
                    // increment the version numbers based on the second argument
                    versionController.incrementVersion(args[1].ToLower());
                    // rebuild the version numbers string
                    versionController.rebuildVersionNumbers();
                    // write the new string to the file overwriting the previous value
                    io.writeStringToFile(versionController.getNewVersion());

                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("The application was unable to extract the version number.");
                    Console.ReadLine();
                }
            }
        }
    }
}
