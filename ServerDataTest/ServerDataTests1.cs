using ServerData;
namespace ServerDataTest
{
    [TestClass]
    public class ServerDataTests1
    {
        private DataAbstractApi PrepareData()
        {
            DataAbstractApi data = DataAbstractApi.Create();
            return data;
        }
        [TestMethod]
        public void AddCandidate()
        {
            DataAbstractApi data = PrepareData();

            data.GetCandidateRepository().AddCandidate(10, "Zbysiu");

            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().ElementAt(5).Name, "Zbysiu");
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().ElementAt(5).Id, 10);

        }
        [TestMethod]
        public void CreateCandidates()
        {
            DataAbstractApi data = PrepareData();
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().Count(), 5);
        }

        [TestMethod]
        public void DeleteCandidate()
        {
            DataAbstractApi data = PrepareData();

            data.GetCandidateRepository().RemoveCandidate(1);
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().Count(), 4);
        }
    }
}