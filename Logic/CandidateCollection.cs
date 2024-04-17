using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;

namespace ClientLogic
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


           // throw new NotImplementedException();
            //List<ICandidatePerson> candidates;
           /* foreach(ICandidateModel candidate in _candidateRepository.GetAllCandidates())
            {
                ICandidatePerson(candidate)

                candidates.Add(CandidatePerson(candidate));
            }
            */
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
    }
}
