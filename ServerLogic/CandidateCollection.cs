using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;

namespace ServerLogic
{
    internal class CandidateCollection : ICandidateCollection
    {
        private readonly ICandidateRepository _candidateRepository;

        public event EventHandler<LogicDaysToElectionChangedEventArgs> DaysToElectionChanged;

        public CandidateCollection(ICandidateRepository candidateRepository)
        {
            this._candidateRepository = candidateRepository;

            _candidateRepository.DaysToElectionChanged += HandleOnDaysChanged;
        }

        private void HandleOnDaysChanged(object sender, DaysToElectionChangedEventArgs args)
        {
            DaysToElectionChanged?.Invoke(this, new LogicDaysToElectionChangedEventArgs(args));
        }

        public void AddVote(int id)
        {
            _candidateRepository.AddVote(id);
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

    }
}
