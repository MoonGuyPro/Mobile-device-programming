using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;

namespace Data
{
    internal class CandidateApi : CandidateRepositoryAbstract
    {
        private readonly CandidateRepository repository;

        public CandidateApi()
        {
            repository = new CandidateRepository();
        }

        public override ICandidateRepository GetCandidateRepository()
        {
            return repository;
        }
    }
}
