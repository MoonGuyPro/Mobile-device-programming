using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Interfaces
{
    public interface IVoteRepository
    {
        List<VoteModel> GetAllVotes();
        VoteModel GetVoteById(int id);
        void AddVote(VoteModel vote);
        void UpdateVote(VoteModel vote);
        void DeleteVote(int id);
    }
}
