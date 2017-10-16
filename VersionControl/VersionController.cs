using System;
using System.Collections.Generic;

/* The purpose of this class is to extract, increment and rebuild the version data */

namespace VersionControl
{
    public class VersionController
    {
        private string currentVersion;
        private string newVersion;
        private string release;
        private int major;
        private int majorDigits = 0;
        private int minor;
        private int minorDigits = 0;
        private List<int> splitPoints = new List<int>();
        private IO io;

        public VersionController()
        {
            currentVersion = "No Version";
        }

        // constructor used by unit tests
        public VersionController(string currentVersion)
        {
            this.currentVersion = currentVersion.Trim();
        }

        public void init(string fileName)
        {
            io = new IO(fileName);
            currentVersion = io.extractStringFromFile();
            currentVersion.Trim();
        }

        public void init(string fileName, string filePath)
        {
            io = new IO(fileName, filePath);
            currentVersion = io.extractStringFromFile();
            currentVersion.Trim();
        }

        public void run(string updateType)
        {
            if (updateType.ToLower() != "feature" && updateType.ToLower() != "bugfix")
            {
                Console.WriteLine("Invalid update type, please enter a type of feature or bugfix");
                Console.ReadLine();
            }
            else
            {
                // extract the version string
                currentVersion = io.extractStringFromFile();
                
      
                // if an exception was thrown while trying to extract the version number skip to the end
                if (currentVersion != "")
                {
                    Console.WriteLine("\nCurrent version of {0} is: {1}", io.getFile(), currentVersion);
                    Console.WriteLine("Update type: {0}", updateType);

                    // split the version numbers from the string
                    extractVersionNumbers();
                    // increment the version numbers based on the second argument
                    incrementVersion(updateType.ToLower());
                    // rebuild the version numbers string
                    rebuildVersionNumbers();
                    // write the new string to the file overwriting the previous value
                    io.writeStringToFile(newVersion);

                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("The application was unable to extract the version number.");
                    Console.ReadLine();
                }
            }
        }

        // splits the version string into three parts, release, major and minor
        public void extractVersionNumbers()
        {
            // store the index of each '.' in the string
            for (int i = 0; i < currentVersion.Length; i++)
            {
                if (currentVersion[i] == '.')
                {
                    splitPoints.Add(i);
                    Console.WriteLine(currentVersion[i]);
                }
            }
            if (splitPoints.Count == 3)
            {
                numOfDigits();

                int offset = 1;
                // extract release version number
                release = currentVersion.Substring(0, splitPoints[1] + offset);
                // extract the major number
                major = int.Parse(currentVersion.Substring(splitPoints[1] + offset, majorDigits));  // to get the second argument you need to find the amount of characters between version numbers
                                                                                                    // extract the minor number
                minor = int.Parse(currentVersion.Substring(splitPoints[2] + offset, minorDigits));
            }
            else
            {
                release = "";
                major = 0;
                minor = 0;
            }
        }

        public void rebuildVersionNumbers()
        {
            newVersion = release + major + "." + minor;
        }

        // increments the version numbers based on the type of version change passed in
        public void incrementVersion(string version)
        {
            if (version == "feature")
            {
                incrementMajor();
            }
            else if (version == "bugfix")
            {
                incrementMinor();
            }
            else
            {
                Console.WriteLine("Invalid version type, input Feature or Bugfix");
            }
        }

        // used to find the number of digits for major and minor numbers
        private void numOfDigits()
        {
            int offset = 1;
            for (int i = splitPoints[1]; i < splitPoints[2] - offset; i++)
            {
                majorDigits++;
            }

            for (int i = splitPoints[2]; i < currentVersion.Length - offset; i++)
            {
                minorDigits++;
            }

        }

        private void incrementMajor()
        {
            major++;
            minor = 0;
        }

        private void incrementMinor()
        {
            minor++;
        }

        public string getVersion()
        {
            return currentVersion;
        }

        public string getNewVersion()
        {
            return newVersion;
        }

    }
}
