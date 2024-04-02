using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models;

namespace Data.Repositories
{
    public class InMemoryVoteRepository : IVoteRepository
    {
        private List<VoteModel> _votes;

        public InMemoryVoteRepository()
        {
            _votes = new List<VoteModel>();
            // Inicjalizacja głosów
        }

        public List<VoteModel> GetAllVotes()
        {
            return _votes;
        }

        public VoteModel GetVoteById(int id)
        {
            return _votes.FirstOrDefault(v => v.Id == id);
        }

        public void AddVote(VoteModel vote)
        {
            _votes.Add(vote);
        }

        public void UpdateVote(VoteModel vote)
        {
            var existingVote = _votes.FirstOrDefault(v => v.Id == vote.Id);
            if (existingVote != null)
            {
                // Aktualizacja danych głosu
                existingVote.CandidateId = vote.CandidateId;
                // inne właściwości
            }
        }

        public void DeleteVote(int id)
        {
            _votes.RemoveAll(v => v.Id == id);
        }
    }
}
