using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Interfaces;
using Data.Models;
using Data.Interfaces;

namespace Logic.Services
{
    public class ElectionService : IElectionService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IVoteRepository _voteRepository;

        public ElectionService(ICandidateRepository candidateRepository, IVoteRepository voteRepository)
        {
            _candidateRepository = candidateRepository;
            _voteRepository = voteRepository;
        }

        public List<CandidateModel> GetAllCandidates()
        {
            return _candidateRepository.GetAllCandidates();
        }

        public CandidateModel GetCandidateById(int id)
        {
            return _candidateRepository.GetCandidateById(id);
        }

        public void AddCandidate(CandidateModel candidate)
        {
            _candidateRepository.AddCandidate(candidate);
        }

        public void UpdateCandidate(CandidateModel candidate)
        {
            _candidateRepository.UpdateCandidate(candidate);
        }

        public void DeleteCandidate(int id)
        {
            _candidateRepository.DeleteCandidate(id);
        }

        public List<VoteModel> GetAllVotes()
        {
            return _voteRepository.GetAllVotes();
        }

        public VoteModel GetVoteById(int id)
        {
            return _voteRepository.GetVoteById(id);
        }

        public void AddVote(VoteModel vote)
        {
            _voteRepository.AddVote(vote);
        }

        public void UpdateVote(VoteModel vote)
        {
            _voteRepository.UpdateVote(vote);
        }

        public void DeleteVote(int id)
        {
            _voteRepository.DeleteVote(id);
        }

        // Implementuj dodatkowe metody związane z logiką wyborczą
    }
}
