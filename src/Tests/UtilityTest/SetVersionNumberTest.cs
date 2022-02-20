using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FCTests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void SetBuildNumberTo42()
        {
            var util = new SetVersionNumber();
            util.BuildNumber = 42;
            util.VersionFile = "GlobalVersion.cs";
            Assert.IsTrue(util.Execute());
        }
    }
}