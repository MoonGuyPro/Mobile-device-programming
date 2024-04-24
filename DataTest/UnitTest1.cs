using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ClientData;
using ClientDataTest;
namespace DataTest
{
    [TestClass]
    public class UnitTest1
    {
        static ConnectionServiceMock connectionService = new ConnectionServiceMock();
        DataAbstractApi data = DataAbstractApi.Create(connectionService);

        public void PrepareData()
        {
            connectionService.MockUpdateAll(new[]
            {
                new ConnectionServiceMock.CandidateDTOMock { Id = 1, Name = "Andrzej" },
                new ConnectionServiceMock.CandidateDTOMock { Id = 2, Name = "Boles³aw" }
            });


        }

        [TestMethod]
        public void MockUpdateAll()
        {
            // Przygotowanie danych wejœciowych
            System.Collections.Generic.ICollection<ConnectionServiceMock.CandidateDTOMock> candidatesDTO = new[]
            {
                new ConnectionServiceMock.CandidateDTOMock { Id = 1, Name = "Andrzej" },
                new ConnectionServiceMock.CandidateDTOMock { Id = 2, Name = "Boles³aw" }
            };

            connectionService.MockUpdateAll(candidatesDTO);

            List<ICandidateModel> candidateModels = data.GetCandidateRepository().GetAllCandidates();

            Assert.AreEqual(2, candidateModels.Count);
        }

        [TestMethod]
        public void AddCandidate()
        {
           // DataAbstractApi data = PrepareData();

            data.GetCandidateRepository().AddCandidate(10,"Zbysiu");
          
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().ElementAt(1).Name, "Zbysiu");
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().ElementAt(1).Id, 10);

        }
        [TestMethod]
        public void CreateCandidates()
        {
           // DataAbstractApi data = PrepareData();
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().Count(), 1);
        }

        [TestMethod]
        public void DeleteCandidate()
        {
            //DataAbstractApi data = PrepareData();

            data.GetCandidateRepository().RemoveCandidate(1);
            Assert.AreEqual(data.GetCandidateRepository().GetAllCandidates().Count(), 0);
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