using ServerData;
namespace ServerDataTest
{
    [TestClass]
    public class ServerDataTests1
    {
        private CandidateRepositoryAbstract PrepareData()
        {
            CandidateRepositoryAbstract data = CandidateRepositoryAbstract.Create();
            return data;
        }
        [TestMethod]
        public void AddCandidate()
        {
            CandidateRepositoryAbstract data = PrepareData();

            data.GetCandidateRepository().AddCandidate(10, "Zbysiu");

            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().ElementAt(5).Name, "Zbysiu");
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().ElementAt(5).Id, 10);

        }
        [TestMethod]
        public void CreateCandidates()
        {
            CandidateRepositoryAbstract data = PrepareData();
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().Count(), 5);
        }

        [TestMethod]
        public void DeleteCandidate()
        {
            CandidateRepositoryAbstract data = PrepareData();

            data.GetCandidateRepository().RemoveCandidate(1);
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().Count(), 4);
        }
    }
}