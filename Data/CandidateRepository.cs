using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;
using System.Collections.ObjectModel;
using ClientApi;

namespace ClientData
{
    internal class CandidateRepository : ICandidateRepository
    {
        private List<ICandidateModel> _candidates = new List<ICandidateModel>();
        private HashSet<IObserver<DaysToElectionChangedEventArgs>> observers;

        private readonly IConnectionService connectionService;

        private object electionLock = new object();
        private object candidatesLock = new object();
        private object voteLock = new object();


        public event Action? CandidatesUpdated;

        public int daysToElection = 220;


        public CandidateRepository(IConnectionService connectionService)
        {
            observers = new HashSet<IObserver<DaysToElectionChangedEventArgs>>();

            AddCandidate(1, "Default Candidate");
            //AddCandidate(2, "Candidate 2");
            //AddCandidate(3, "Candidate 3");
            //AddCandidate(4, "Candidate 4");
            //AddCandidate(5, "Candidate 5");


            this.connectionService = connectionService;
            this.connectionService.OnMessage += OnMessage;
        }

        ~CandidateRepository()
        {
            List<IObserver<DaysToElectionChangedEventArgs>> cachedObservers = observers.ToList();
            foreach (IObserver<DaysToElectionChangedEventArgs>? observer in cachedObservers)
            {
                observer?.OnCompleted();
            }
        }

        private void OnMessage(string message)
        {
            Serializer serializer = Serializer.Create();
            if (serializer.GetResponseHeader(message) == ServerApi.UpdateAllResponceHeader)
            {
                System.Diagnostics.Debug.WriteLine($"cands");
                UpdateAllResponce responce = serializer.Deserialize<UpdateAllResponce>(message);
                UpdateAllCandidates(responce);
            } else if (serializer.GetResponseHeader(message) == ServerApi.VoringReminderHeader)
            {
                VotingReminder responce = serializer.Deserialize<VotingReminder>(message);
                DaysToElection(responce);
            }
            else 
            {
                Task.Run(() => RequestCandidates());
            }

        }

        private void DaysToElection(VotingReminder responce)
        {
            if (responce.DaysToElection == null)
                return;

            lock (electionLock)
            {
                daysToElection = responce.DaysToElection;
            }

            foreach(IObserver<DaysToElectionChangedEventArgs> observer in observers)
            {
                observer.OnNext(new DaysToElectionChangedEventArgs(responce.DaysToElection));
            }

        }

        private void UpdateAllCandidates(UpdateAllResponce responce)
        {
            if (responce.Candidates == null)
                return;
            lock (candidatesLock)
            {
                _candidates.Clear();
                foreach (CandidateDTO candidate in responce.Candidates)
                {
                    _candidates.Add(candidate.ToCandidate());
                }
            }
            CandidatesUpdated?.Invoke();
        }

        public void AddCandidate(int id, string name)
        {
            lock (candidatesLock)
            {
                _candidates.Add(new CandidateModel(id, name));
            }
            
        }

        public List<ICandidateModel> GetAllCandidates()
        {
            List<ICandidateModel> candidates = new List<ICandidateModel>();
            lock(candidatesLock)
            {
                candidates.AddRange(_candidates);
            }
            

            return candidates;
        }

        public void RemoveCandidate(int id)
        {
            lock (candidatesLock)
            {
                _candidates.RemoveAt(id - 1);
            }
            
        }

        public int GetVotesNumberForCandidate(int id)
        {
            lock (voteLock)
            {
                foreach (CandidateModel candidate in _candidates)
                {
                    if (candidate.Id == id)
                        return candidate.VotesNumber;
                }
                return 0;
            }

        }

        public void AddVote(int id)
        {
            lock (voteLock)
            {
                foreach (CandidateModel candidate in _candidates)
                {
                    if (candidate.Id == id)
                        candidate.VotesNumber++;
                }
            }

        }

        public int getDays()
        {
            lock (electionLock)
            {
                return daysToElection;
            }
            
        }
        public async Task RequestCandidates()
        {
            Serializer serializer = Serializer.Create();
            GetCandidatesCommand itemsCommand = new GetCandidatesCommand { Header = ServerApi.GetCandidatesCommandHeader };
            System.Diagnostics.Debug.WriteLine($"it wants to get the cands");
            await connectionService.SendAsync(serializer.Serialize(itemsCommand));
        }

        public void RequestUpdate()
        {
            if (connectionService.IsConnected())
            {
                Task task = Task.Run(async () => await RequestCandidates());
            }
        }

        public async Task VoteForCandidate(int id)
        {
            if (connectionService.IsConnected())
            {
                Serializer serializer = Serializer.Create();
                VoteForCandidateCommand voteForCandidateCommand = new VoteForCandidateCommand();
                voteForCandidateCommand.CandidateId = id;
                await connectionService.SendAsync(serializer.Serialize(voteForCandidateCommand));   

            }
        }

        public IDisposable Subscribe(IObserver<DaysToElectionChangedEventArgs> observer)
        {
            observers.Add(observer);
            return new CandidateRepositoryDisposable(this, observer);
        }

        private void UnSubscribe(IObserver<DaysToElectionChangedEventArgs> observer)
        {
            observers.Remove(observer);
        }

        private class CandidateRepositoryDisposable : IDisposable
        {
            private readonly CandidateRepository candidateRepository;
            private readonly IObserver<DaysToElectionChangedEventArgs> observer;

            public CandidateRepositoryDisposable(CandidateRepository candidateRepository, IObserver<DaysToElectionChangedEventArgs> observer)
            {
                this.candidateRepository = candidateRepository;
                this.observer = observer;
            }
            public void Dispose()
            {
                candidateRepository.UnSubscribe(observer);
            }
        }
    }
}
