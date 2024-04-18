using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientLogic;

namespace Model
{
    public class CandidateRepositoryPresentation
    {
        private ICandidateCollection _candidateCollection { get; set; }
        public event EventHandler<ModelDaysToElectionChangedEventArgs> DaysToElectionChanged;
        public Action? OnCandidatesUpdated;

        public CandidateRepositoryPresentation(ICandidateCollection candidateCollection)
        {
            this._candidateCollection = candidateCollection;
            candidateCollection.CandidatesUpdated += () => OnCandidatesUpdated?.Invoke();
            candidateCollection.daysToElectionChanged += (obj, args) => DaysToElectionChanged?.Invoke(this, new ModelDaysToElectionChangedEventArgs(args));
        }

        public List<CandidatePresentation> GetCandidates()
        {
            return _candidateCollection.GetCandidates()
                                            .Select(candidate => new CandidatePresentation(candidate))
                                            .Cast<CandidatePresentation>()
                                            .ToList();
        }

        public int GetVotesNumberForCandidate(int id)
        {
            int votes = _candidateCollection.GetVotesForCandidate(id);
            return votes;
        }
        public void AddVote(int id)
        {
            _candidateCollection.AddVote(id);
        }

        public void RequestUpdate()
        {
            _candidateCollection.RequestUpdate();
        }

        public int getDays()
        {
             return  _candidateCollection.getDays();
        }
    }
}
