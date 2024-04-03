using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Logic.Interfaces
{
    public interface IElectionService
    {
        List<CandidateModel> GetAllCandidates();
        CandidateModel GetCandidateById(int id);
        void AddCandidate(CandidateModel candidate);
        void UpdateCandidate(CandidateModel candidate);
        void DeleteCandidate(int id);

        List<VoteModel> GetAllVotes();
        VoteModel GetVoteById(int id);
        void AddVote(VoteModel vote);
        void UpdateVote(VoteModel vote);
        void DeleteVote(int id);
        Task SendVotingReminderPeriodically();
        event EventHandler<int> UpdateDaysToElection;
    }
}
