using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;

namespace ServerData
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

        public List<IVoteModel> GetVotesForCandidate(int id)
        {
            List<IVoteModel> votes = new List<IVoteModel>();

            foreach (VoteModel vote in _votes)
            {
                if (vote.Id == id)
                {
                    votes.Add(vote);
                }
            }

            return votes;
        }
    }
}
