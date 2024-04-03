using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Interfaces;
using Logic.Services;

namespace Logic.Interfaces
{
    public abstract class IElectionService
    {
        public abstract List<CandidateModel> GetAllCandidates();
        public abstract CandidateModel GetCandidateById(int id);
        public abstract void AddCandidate(CandidateModel candidate);
        public abstract void UpdateCandidate(CandidateModel candidate);
        public abstract void DeleteCandidate(int id);

        public abstract List<VoteModel> GetAllVotes();
        public abstract VoteModel GetVoteById(int id);
        public abstract void AddVote(VoteModel vote);
        public abstract void UpdateVote(VoteModel vote);
        public abstract void DeleteVote(int id);
        public abstract Task SendVotingReminderPeriodically();
        public abstract event EventHandler<int> UpdateDaysToElection;

        public static IElectionService CreateInstance(ICandidateRepository _candidateRepository, IVoteRepository _voteRepository)
        {
            return new ElectionService(_candidateRepository, _voteRepository);
        }
    }
}
