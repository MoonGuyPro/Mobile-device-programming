using ServerData;
using ServerLogic;
namespace ServerLogicTest
{
    public class CandidateRepositoryMock : DataAbstractApi
    {
        private readonly CandidateRepoMock repoMock = new CandidateRepoMock();

        public override ICandidateRepository GetCandidateRepository()
        {
            return repoMock;
        }
    }

    public class CandidateMock : ICandidateModel
    {
        public CandidateMock(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int VotesNumber { get; set; }
        }


    public class CandidateRepoMock : ICandidateRepository
    {
        private readonly List<ICandidateModel> allCandidates;

        public CandidateRepoMock()
        {
            allCandidates = new List<ICandidateModel>
            {
                new CandidateMock(1,"Candidate 1"),
                new CandidateMock(2,"Candidate 2"),
                new CandidateMock(3,"Candidate 3")
            };

        }
        public void AddCandidate(int id, string name)
        {
            var existingCandidate = allCandidates.FirstOrDefault(c => c.Id == id);
            if (existingCandidate != null)
            {
                throw new ArgumentException($"A candidate with ID {id} already exists.");
            }

            allCandidates.Add(new CandidateMock(id, name));
        }

        public void AddVote(int id)
        {
            throw new NotImplementedException();
        }

        public List<ICandidateModel> GetAllCandidates()
        {
            return allCandidates;
        }

        public int GetVotesNumberForCandidate(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveCandidate(int id)
        {
            var candidateToRemove = allCandidates.FirstOrDefault(c => c.Id == id);
            if (candidateToRemove == null)
            {
                throw new ArgumentException($"A candidate with ID {id} does not exist.");
            }

            allCandidates.Remove(candidateToRemove);
        }
    }

    [TestClass]
    public class UnitTest1
    {
        private LogicAbstractApi ESAbstract = LogicAbstractApi.Create(new CandidateRepositoryMock());

        [TestMethod]
        public void CheckCandidate()
        {
            //System.Diagnostics.Debug.WriteLine($"Voted for candidate ID: {ESAbstract.GetCandidates().GetCandidates().Count}");

            Assert.IsTrue(ESAbstract.GetCandidates().GetCandidates().Count == 3);
            Assert.IsTrue(ESAbstract.GetCandidates().GetCandidates().First().Id == 1);
            Assert.IsTrue(ESAbstract.GetCandidates().GetCandidates().First().Name == "Candidate 1");
            Assert.IsTrue(ESAbstract.GetCandidates().GetCandidates().Last().Id == 3);
            //ESAbstract.GetCandidates().GetCandidates();
        }
    }
}