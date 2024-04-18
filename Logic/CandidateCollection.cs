using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;

namespace ClientLogic
{
    internal class CandidateCollection : ICandidateCollection, IObserver<DaysToElectionChangedEventArgs>
    {
        private readonly ICandidateRepository _candidateRepository;
        public event Action? CandidatesUpdated;
        public event EventHandler<LogicDaysToElectionChangedEventArgs> daysToElectionChanged;

        private IDisposable CandidateRepoSubscriptionHandle;

        public CandidateCollection(ICandidateRepository candidateRepository)
        {
            this._candidateRepository = candidateRepository;

            CandidateRepoSubscriptionHandle = _candidateRepository.Subscribe(this);

            candidateRepository.CandidatesUpdated += () => CandidatesUpdated?.Invoke();
        }

        public List<ICandidatePerson> GetCandidates()
        {
            return _candidateRepository.GetAllCandidates()
                .Select(candidatePerson => new CandidatePerson(candidatePerson))
                .Cast<ICandidatePerson>().ToList();
        }


        public int GetVotesForCandidate(int id)
        {
            return _candidateRepository.GetVotesNumberForCandidate(id);
        }

        public void AddVote(int id)
        {
            _candidateRepository.AddVote(id);
        }

        public async Task VoteForCandidate(int id)
        {
            await _candidateRepository.VoteForCandidate(id);
        }

        public void RequestUpdate()
        {
            _candidateRepository.RequestUpdate();
        }
        public int getDays()
        {
            return _candidateRepository.getDays();
        }

        public void OnCompleted()
        {
            CandidateRepoSubscriptionHandle.Dispose();
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(DaysToElectionChangedEventArgs value)
        {
            daysToElectionChanged?.Invoke(this, new LogicDaysToElectionChangedEventArgs(value));
        }
    }
}
