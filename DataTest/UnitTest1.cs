using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Data;

namespace DataTest
{
    [TestClass]
    public class UnitTest1
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

            data.GetCandidateRepository().AddCandidate(10,"Zbysiu");
          
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

        /*
        [TestMethod]
        public void AddAndGetCandidate()
        {
            // Arrange
            var repository = new InMemoryCandidateRepository();
            var candidate = new CandidateModel { Id = 1, Name = "John Doe" };

            // Act
            repository.AddCandidate(candidate);
            var retrievedCandidate = repository.GetCandidateById(1);

            // Assert
            Assert.IsNotNull(retrievedCandidate);
            Assert.AreEqual("John Doe", retrievedCandidate.Name);
        }

        [TestMethod]
        public void DeleteCandidate()
        {
            // Arrange
            var repository = new InMemoryCandidateRepository();
            var candidate = new CandidateModel { Id = 1, Name = "John Doe" };
            repository.AddCandidate(candidate);

            // Act
            repository.DeleteCandidate(1);
            var retrievedCandidate = repository.GetCandidateById(1);

            // Assert
            Assert.IsNull(retrievedCandidate);
        }

        [TestMethod]
        public void AddAndGetVote()
        {
            // Arrange
            var repository = new InMemoryVoteRepository();
            var vote = new VoteModel { Id = 1, CandidateId = 1 };

            // Act
            repository.AddVote(vote);
            var retrievedVote = repository.GetVoteById(1);

            // Assert
            Assert.IsNotNull(retrievedVote);
            Assert.AreEqual(1, retrievedVote.CandidateId);
        }

        [TestMethod]
        public void DeleteVote()
        {
            // Arrange
            var repository = new InMemoryVoteRepository();
            var vote = new VoteModel { Id = 1, CandidateId = 1 };
            repository.AddVote(vote);

            // Act
            repository.DeleteVote(1);
            var retrievedVote = repository.GetVoteById(1);

            // Assert
            Assert.IsNull(retrievedVote);
        }*/
    }
}