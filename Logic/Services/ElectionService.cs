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
        public event EventHandler<int> UpdateDaysToElection;

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

        public async Task SendVotingReminderPeriodically()
        {
            while (true)
            {
                DateTime electionDay = new DateTime(2024, 05, 10); // Przykładowa data wyborów
                TimeSpan timeRemaining = electionDay - DateTime.Today; // Czas pozostały do wyborów
                System.Diagnostics.Debug.WriteLine(timeRemaining);

                // Aktualizacja liczby dni pozostałych do wyborów w MainViewModel
                UpdateDaysToElection?.Invoke(this, timeRemaining.Days);

                // Poczekaj jeden dzień
                await Task.Delay(TimeSpan.FromDays(1)); // Sprawdź co dzień
            }
        }
    }
}
