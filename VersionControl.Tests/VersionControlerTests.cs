using NUnit.Framework;

namespace VersionControl.Tests
{
    [TestFixture]
    public class VersionControlerTests
    {
        /* [unitOfWork_stateUnderTest_expectedBehavior] */

        // "0.01.01.01" test failed the trailing zero is removed from major/minor
        [Test]
        public void extractVersionNumbers_whenVersionFormatCorrect_valueDoesMatch
            ([Values("5.39.8.0", "5.656.895.11", "0.01.01.01", "0.0.0.0",
                     "1.1.99999999.99999999", " 1.1.1.1 ")]string input)
        {
            VersionController vc = new VersionController(input);
            vc.extractVersionNumbers();
            vc.rebuildVersionNumbers();
            Assert.AreEqual(vc.getVersion(), vc.getNewVersion());
        }

        [Test]
        public void extractVersionNumbers_whenVersionFormatIncorrect_valueDoesNotMatch
            ([Values("1","1111","1.1", "1.1.1", "1.1.1.1.1", "1 . 1. 1 .1")]string input)
        {
            VersionController vc = new VersionController(input);
            vc.extractVersionNumbers();
            vc.rebuildVersionNumbers();
            Assert.AreNotEqual(vc.getVersion(), vc.getNewVersion());
        }
        

        [Test]
        public void incrementVersion_whenTypeOfReleaseIsFeature_valueDoesMatch([Values("feature")]string input)
        {
            VersionController vc = new VersionController("5.39.8.0");
            vc.extractVersionNumbers();
            vc.incrementVersion(input);
            vc.rebuildVersionNumbers();
            Assert.AreEqual("5.39.9.0", vc.getNewVersion());
        }

        [Test]
        public void incrementVersion_whenTypeOfReleaseIsBugfix_valueDoesMatch([Values("bugfix")]string input)
        {
            VersionController vc = new VersionController("5.39.8.0");
            vc.extractVersionNumbers();
            vc.incrementVersion(input);
            vc.rebuildVersionNumbers();
            Assert.AreEqual("5.39.8.1", vc.getNewVersion());
        }

    }
}
