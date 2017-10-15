using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionControl;

namespace VersionControl.Tests
{
    [TestFixture]
    class IOTests
    {
        /* [UnitOfWork_StateUnderTest_ExpectedBehavior] */

        [Test]
        public void extractStringFromFile_whenNoFilePath_returnsEmptyString()
        {
            IO io = new IO();
            string output = io.extractStringFromFile();
            Assert.AreEqual("", output);

        }

        [Test]
        public void extractStringFromFile_whenIncorrectFilePathSpecified_returnsEmptyString
            ([Values("ProductInfoTestIO.cs")]string input1, 
            [Values("C:\\Users\\", "C:\\Users\\NotAFilePath", "Users")]string input2)
        {
            IO io = new IO(input1, input2);
            string output = io.extractStringFromFile();
            Assert.AreEqual("", output);
        }

        [Test]
        public void extractStringFromFile_whenIncorrectFileSpecified_returnsEmptyString
            ([Values("ProductInflowTesttIOO.cs")]string input1)
        {
            IO io = new IO(input1);
            string output = io.extractStringFromFile();
            Assert.AreEqual("", output);
        }

        [Test]
        public void extractStringFromFile_whenCorrectFilePathSpecified_returnsString
                    ([Values("ProductInfoTestIO.cs")]string input1,
                     [Values("C:\\Users\\Jayden\\Documents\\Visual Studio 2013\\Projects\\VersionControl\\VersionControl.Tests\\bin\\Debug")]string input2)
        {
            IO io = new IO(input1, input2);
            string output = io.extractStringFromFile();
            Assert.AreEqual("5.39.8.0", output);
        }

        // failing because the current directory of the running unit tests is different
        [Test]
        public void extractStringFromFile_whenCorrectFileSpecified_returnsString
                    ([Values("ProductInfoTestIO.cs")]string input1)
        {
            IO io = new IO(input1);
            string output = io.extractStringFromFile();
            Assert.AreEqual("5.39.8.0", output);
        }

        // should allow the user to just specify filepath as one argument
        [Test]
        public void writeStringToFile_whenCorrectValueWritten_valueCorrect([Values("Test String", "1.5.3.2", "!£$%^&*123456789abcdefg")]string input)
        {
            IO io = new IO("ProductInfoTestIOWrite.cs", 
                "C:\\Users\\Jayden\\Documents\\Visual Studio 2013\\Projects\\VersionControl\\VersionControl.Tests\\bin\\Debug");
            io.writeStringToFile(input);
            
            StreamReader sr = File.OpenText(io.getFilePath());
            Assert.AreEqual(input, sr.ReadLine());
            sr.Close();
        }

    

    }
}
