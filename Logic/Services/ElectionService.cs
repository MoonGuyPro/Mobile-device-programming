using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data;
using Data.Interfaces;

namespace Logic.Services
{
    internal class ElectionService : ElectionServiceAbstract
    {
        private readonly CandidateRepositoryAbstract _candidateRepository;
        private readonly IVoteRepository _voteRepository;
        public override event EventHandler<int> UpdateDaysToElection;

        public ElectionService(CandidateRepositoryAbstract? candidateRepository, IVoteRepository voteRepository)
        {
           _candidateRepository = candidateRepository;
            _voteRepository = voteRepository;
        }

        public override List<CandidateModel> GetAllCandidates()
        {
            return _candidateRepository.GetAllCandidates();
        }

        public override CandidateModel GetCandidateById(int id)
        {
            return _candidateRepository.GetCandidateById(id);
        }

        public override void AddCandidate(CandidateModel candidate)
        {
            _candidateRepository.AddCandidate(candidate);
        }

        public override void UpdateCandidate(CandidateModel candidate)
        {
            _candidateRepository.UpdateCandidate(candidate);
        }

        public override void DeleteCandidate(int id)
        {
            _candidateRepository.DeleteCandidate(id);
        }

        public override List<VoteModel> GetAllVotes()
        {
            return _voteRepository.GetAllVotes();
        }

        public override VoteModel GetVoteById(int id)
        {
            return _voteRepository.GetVoteById(id);
        }

        public override void AddVote(VoteModel vote)
        {
            _voteRepository.AddVote(vote);
        }

        public override void UpdateVote(VoteModel vote)
        {
            _voteRepository.UpdateVote(vote);
        }

        public override void DeleteVote(int id)
        {
            _voteRepository.DeleteVote(id);
        }

        public override async Task SendVotingReminderPeriodically()
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
