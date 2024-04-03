using Data.Models;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DataTest
{
    [TestClass]
    public class UnitTest1
    {
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
        }
    }
}