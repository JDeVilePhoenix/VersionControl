using System;
using System.IO;

/* The purpose of this class is to extract and write strings from a file */

namespace VersionControl
{
    public class IO
    {
        private string file;
        private string path;
        private string filePath;

        public IO()
        {
            file = "No file";
            path = "No path";
            filePath = "No file path";
        }

        public IO(string file, string path)
        {
            this.file = file;
            this.path = path;
            filePath = path + "\\" + file;
        }

        public IO(string file)
        {
            this.file = file;
            this.path = Directory.GetCurrentDirectory();
            filePath = path + "\\" + file;
        }

        public string extractStringFromFile()
        {
            /* open the file read the first line and extract the contents */
            string extractedValue = "";
            try
            {
                StreamReader sr = File.OpenText(filePath);

                extractedValue = sr.ReadLine();

                sr.Close();

                return extractedValue;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find the file specified," 
                                + " check that your file name is correct: {0}", e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Could not find the file specified,"
                                + " check that the file exists in the application directory,"
                                + " if a directory was specified check that this is correct: {0}", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occured: {0}", e.Message);
            }

            // return an empty string if an exception was thrown
            return extractedValue;
        }

        public void writeStringToFile(string stringValue)
        {
            StreamWriter sw = new StreamWriter(filePath, false);

            sw.WriteLine(stringValue);

            sw.Close();

            Console.WriteLine("New Version: {0}", stringValue);
        }

        public string getFile()
        {
            return file;
        }

        public string getFilePath()
        {
            return filePath;
        }

    }
}
