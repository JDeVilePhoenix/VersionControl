using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// write description of the class
// add error checking

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

        public VersionController()
        {
            currentVersion = "No Version";
        }

       
        public VersionController(string currentVersion)
        {
            this.currentVersion = currentVersion.Trim();
        }

        public void extractVersionNumbers()
        {
            
            for (int i = 0; i < currentVersion.Length; i++)
            {
                if (currentVersion[i] == '.')
                {
                    splitPoints.Add(i);
                    Console.WriteLine(currentVersion[i]);
                }
            }
            Console.WriteLine("splitPoints capacity: {0}", splitPoints.Count);
            if (splitPoints.Count == 3)
            {
                numOfDigits();

                int offset = 1;
                // extract left part version number
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
