using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
    internal class VoteRepository : IVoteRepository
    {
        private List<VoteModel> _votes = new List<VoteModel>();

        public VoteRepository()
        {

        }

        public void AddVote(int id, int candidateId)
        {
            _votes.Add(new VoteModel(id, candidateId));
        }

        public List<IVoteModel> GetAllVotes()
        {
            List<IVoteModel> votes = new List<IVoteModel>();

            votes.AddRange(_votes);

            return votes;
        }

        public IVoteModel GetVotesForCandidate(int id)
        {
            throw new NotImplementedException();
        }
    }
}
