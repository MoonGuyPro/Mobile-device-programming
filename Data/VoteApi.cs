using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;

namespace Data
{
    internal class VoteApi : VoteRepositoryAbstract
    {
        private readonly VoteRepository repository;

        public VoteApi()
        {
            repository = new VoteRepository();
        }
        public override IVoteRepository GetVoteRepository()
        {
            return repository;
        }
    }
}
