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

        public CandidateCollection(ICandidateRepository candidateRepository)
        {
            this._candidateRepository = candidateRepository;
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
