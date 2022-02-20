using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FCTests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void SetVersionNumber()
        {
            var util = new SetVersionNumber();
            util.VersionNumber = "12.6.42";
            util.VersionFile = "GlobalVersion.cs";
            Assert.IsTrue(util.Execute());
        }
    }
}