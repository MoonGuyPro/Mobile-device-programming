using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
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
            throw new NotImplementedException();
        }


    }
}
