namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddTest()
        {
            int x = Data.AddClass.Add(2, 2);
            Assert.AreEqual(4, x);
        }
    }
}