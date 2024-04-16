using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using ClientData;

namespace Data
{
    internal class CandidateApi : CandidateRepositoryAbstract
    {
        private readonly CandidateRepository repository;
        private readonly IConnectionService connectionService;

        public CandidateApi()
        {
            repository = new CandidateRepository(connectionService);
        }

        public override ICandidateRepository GetCandidateRepository()
        {
            return repository;
        }
    }
}
