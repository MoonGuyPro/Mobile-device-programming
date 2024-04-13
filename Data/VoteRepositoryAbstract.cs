using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IVoteModel
    {
        int Id { get; set; }
        int CandidateId { get; set; }
    }

    public interface IVoteRepository
    {
        public List<IVoteModel> GetAllVotes();
        public IVoteModel GetVotesForCandidate(int id);
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
