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

        public CandidateRepositoryPresentation(ICandidateCollection candidateCollection)
        {
            this._candidateCollection = candidateCollection;
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

    }
}
