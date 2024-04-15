using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;

namespace ServerData
{
    public interface IVoteModel
    {
        int Id { get; set; }
        int CandidateId { get; set; }
    }

    public interface IVoteRepository
    {
        public List<IVoteModel> GetAllVotes();
        public List<IVoteModel> GetVotesForCandidate(int id);
        public void AddVote(int id, int candidateId);
    }

    public abstract class VoteRepositoryAbstract
    {
        public static VoteRepositoryAbstract Create()
        {
            return new VoteApi();
        }

        public abstract IVoteRepository GetVoteRepository();
    }
}
