using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ClientLogic;
using ClientData;

namespace LogicTest
{
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

    public class CandidateRepositoryDisposableMock : IDisposable
    {
        private readonly CandidateRepoMock candidateRepository;
        private readonly IObserver<DaysToElectionChangedEventArgs> observer;

        public CandidateRepositoryDisposableMock(CandidateRepoMock candidateRepository, IObserver<DaysToElectionChangedEventArgs> observer)
        {
            this.candidateRepository = candidateRepository;
            this.observer = observer;
        }
        public void Dispose()
        {
            candidateRepository.UnSubscribe(observer);
        }
    }


    public class CandidateRepoMock : ICandidateRepository, IObserver<DaysToElectionChangedEventArgs>
    {
        private readonly List<ICandidateModel> allCandidates;
        private HashSet<IObserver<DaysToElectionChangedEventArgs>> observers;

        public CandidateRepoMock()
        {
            allCandidates = new List<ICandidateModel>
            {
                new CandidateMock(1,"Candidate 1"),
                new CandidateMock(2,"Candidate 2"),
                new CandidateMock(3,"Candidate 3")
            };

        }

        public event Action? CandidatesUpdated;

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

        public int getDays()
        {
            throw new NotImplementedException();
        }

        public int GetVotesNumberForCandidate(int id)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(DaysToElectionChangedEventArgs value)
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

        public void RequestUpdate()
        {
            throw new NotImplementedException();
        }


        public Task VoteForCandidate(int id)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<DaysToElectionChangedEventArgs> observer)
        {
            //observers.Add(observer);
            return new CandidateRepositoryDisposableMock(this, observer);
        }

        public void UnSubscribe(IObserver<DaysToElectionChangedEventArgs> observer)
        {
            //observers.Remove(observer);
        }

    }

    public class ConnectionServiceMock : IConnectionService
    {
        public event Action<string>? Logger;
        public event Action? OnConnectionStateChanged;
        public event Action<string>? OnMessage;
        public event Action? OnError;
        public event Action? OnDisconnect;

        public Task Connect(Uri peerUri)
        {
            throw new NotImplementedException();
        }

        public Task Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string message)
        {
            throw new NotImplementedException();
        }
    }
    public class DataApiMock : DataAbstractApi
    {
        private readonly CandidateRepoMock candidatesMock = new CandidateRepoMock();
        private readonly ConnectionServiceMock connectionServiceMock = new ConnectionServiceMock();

        public override ICandidateRepository GetCandidateRepository()
        {
            return candidatesMock;
        }

        public override IConnectionService GetConnectionService()
        {
            return connectionServiceMock;
        }

    }


    [TestClass]
    public class UnitTest1
    {
       // private ElectionServiceAbstract ESAbstract = ElectionServiceAbstract.Create(new CandidateRepositoryMock());
        private LogicAbstractApi logicApi = LogicAbstractApi.Create(new DataApiMock());
        [TestMethod]
        public void CheckCandidate()
        {
            System.Diagnostics.Debug.WriteLine($"Voted for candidate ID: {logicApi.GetCandidates().GetCandidates().Count}");
            
            Assert.IsTrue(logicApi.GetCandidates().GetCandidates().Count == 3);
            Assert.IsTrue(logicApi.GetCandidates().GetCandidates().First().Id == 1);
            Assert.IsTrue(logicApi.GetCandidates().GetCandidates().First().Name == "Candidate 1");
            Assert.IsTrue(logicApi.GetCandidates().GetCandidates().Last().Id == 3);
            //ESAbstract.GetCandidates().GetCandidates();
        }
    }

    
    // Mock Data Storage
    public class MockDataStorage
    {
        public List<CandidateModel> Candidates { get; set; } = new List<CandidateModel>();
        //public List<VoteModel> Votes { get; set; } = new List<VoteModel>();
    }
}
/*
    // Mock Repositories
    public class MockCandidateRepository : CandidateRepositoryAbstract
    {
        private MockDataStorage _storage;

        public MockCandidateRepository(MockDataStorage storage)
        {
            _storage = storage;
        }

        public void AddCandidate(CandidateModel candidate)
        {
            _storage.Candidates.Add(candidate);
        }

        public void DeleteCandidate(int id)
        {
            _storage.Candidates.RemoveAll(c => c.Id == id);
        }

        public List<CandidateModel> GetAllCandidates()
        {
            return _storage.Candidates;
        }

        public CandidateModel GetCandidateById(int id)
        {
            return _storage.Candidates.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateCandidate(CandidateModel candidate)
        {
            var existing = _storage.Candidates.FirstOrDefault(c => c.Id == candidate.Id);
            if (existing != null)
            {
                existing.Name = candidate.Name;
                // Update other properties as needed
            }
        }
    }

    public class MockVoteRepository : IVoteRepository
    {
        private MockDataStorage _storage;

        public MockVoteRepository(MockDataStorage storage)
        {
            _storage = storage;
        }

        public void AddVote(VoteModel vote)
        {
            _storage.Votes.Add(vote);
        }

        public void DeleteVote(int id)
        {
            _storage.Votes.RemoveAll(v => v.Id == id);
        }

        public List<VoteModel> GetAllVotes()
        {
            return _storage.Votes;
        }

        public VoteModel GetVoteById(int id)
        {
            return _storage.Votes.FirstOrDefault(v => v.Id == id);
        }

        public void UpdateVote(VoteModel vote)
        {
            var existing = _storage.Votes.FirstOrDefault(v => v.Id == vote.Id);
            if (existing != null)
            {
                existing.CandidateId = vote.CandidateId;
                // Update other properties as needed
            }
        }
    }


    [TestClass]
    public class UnitTest1
    {
        private ElectionService _electionService;
        private MockDataStorage _storage;

        [TestInitialize]
        public void Setup()
        {
            _storage = new MockDataStorage();
            _electionService = new ElectionService(new MockCandidateRepository(_storage), new MockVoteRepository(_storage));
        }

        [TestMethod]
        public void GetAllCandidates_ShouldReturnAllCandidates()
        {
            // Arrange
            _storage.Candidates.Add(new CandidateModel { Id = 1, Name = "Candidate 1" });
            _storage.Candidates.Add(new CandidateModel { Id = 2, Name = "Candidate 2" });

            // Act
            var result = _electionService.GetAllCandidates();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetCandidateById_ShouldReturnCorrectCandidate()
        {
            // Arrange
            var expectedCandidate = new CandidateModel { Id = 1, Name = "Candidate 1" };
            _storage.Candidates.Add(expectedCandidate);

            // Act
            var result = _electionService.GetCandidateById(1);

            // Assert
            Assert.AreEqual(expectedCandidate.Name, result.Name);
        }

        [TestMethod]
        public void AddCandidate_ShouldAddCandidateCorrectly()
        {
            // Arrange
            var newCandidate = new CandidateModel { Id = 1, Name = "New Candidate" };

            // Act
            _electionService.AddCandidate(newCandidate);
            var result = _electionService.GetAllCandidates();

            // Assert
            Assert.IsTrue(result.Any(c => c.Name == "New Candidate"));
        }

        [TestMethod]
        public void UpdateCandidate_ShouldUpdateCandidateCorrectly()
        {
            // Arrange
            var existingCandidate = new CandidateModel { Id = 1, Name = "Original Name" };
            _storage.Candidates.Add(existingCandidate);
            var updatedCandidate = new CandidateModel { Id = 1, Name = "Updated Name" };

            // Act
            _electionService.UpdateCandidate(updatedCandidate);
            var result = _electionService.GetCandidateById(1);

            // Assert
            Assert.AreEqual("Updated Name", result.Name);
        }

        [TestMethod]
        public void DeleteCandidate_ShouldRemoveCandidateCorrectly()
        {
            // Arrange
            var candidateToRemove = new CandidateModel { Id = 1, Name = "Candidate 1" };
            _storage.Candidates.Add(candidateToRemove);

            // Act
            _electionService.DeleteCandidate(1);
            var result = _electionService.GetAllCandidates();

            // Assert
            Assert.IsFalse(result.Any(c => c.Id == 1));
        }

        [TestMethod]
        public void GetAllVotes_ShouldReturnAllVotes()
        {
            // Arrange
            _storage.Votes.Add(new VoteModel { Id = 1, CandidateId = 1 });
            _storage.Votes.Add(new VoteModel { Id = 2, CandidateId = 2 });

            // Act
            var result = _electionService.GetAllVotes();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetVoteById_ShouldReturnCorrectVote()
        {
            // Arrange
            var expectedVote = new VoteModel { Id = 1, CandidateId = 1 };
            _storage.Votes.Add(expectedVote);

            // Act
            var result = _electionService.GetVoteById(1);

            // Assert
            Assert.AreEqual(expectedVote.CandidateId, result.CandidateId);
        }

        [TestMethod]
        public void AddVote_ShouldAddVoteCorrectly()
        {
            // Arrange
            var newVote = new VoteModel { Id = 1, CandidateId = 1 };

            // Act
            _electionService.AddVote(newVote);
            var result = _electionService.GetAllVotes();

            // Assert
            Assert.IsTrue(result.Any(v => v.Id == 1));
        }

        [TestMethod]
        public void UpdateVote_ShouldUpdateVoteCorrectly()
        {
            // Arrange
            var existingVote = new VoteModel { Id = 1, CandidateId = 1 };
            _storage.Votes.Add(existingVote);
            var updatedVote = new VoteModel { Id = 1, CandidateId = 2 };

            // Act
            _electionService.UpdateVote(updatedVote);
            var result = _electionService.GetVoteById(1);

            // Assert
            Assert.AreEqual(2, result.CandidateId);
        }

        [TestMethod]
        public void DeleteVote_ShouldRemoveVoteCorrectly()
        {
            // Arrange
            var voteToRemove = new VoteModel { Id = 1, CandidateId = 1 };
            _storage.Votes.Add(voteToRemove);

            // Act
            _electionService.DeleteVote(1);
            var result = _electionService.GetAllVotes();

            // Assert
            Assert.IsFalse(result.Any(v => v.Id == 1));
        }
    }*/
