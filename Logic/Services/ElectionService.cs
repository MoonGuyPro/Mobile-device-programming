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

        // Metoda symulująca okresowe przypomnienie wyborcom o dniu głosowania
        public async Task SendVotingReminderPeriodically()
        {
            while (true)
            {
                // Symulacja wysłania przypomnienia co 7 dni przed dniem wyborów
                DateTime electionDay = new DateTime(2024, 10, 10); // Przykładowa data wyborów
                DateTime reminderDate = electionDay.AddDays(-7); // Przypomnienie 7 dni przed wyborami

                // Sprawdź, czy aktualna data jest równa daty przypomnienia
                if (DateTime.Today == reminderDate)
                {
                    SendVotingReminder();
                }

                await Task.Delay(TimeSpan.FromDays(1)); // Sprawdź co dzień
            }
        }

        // Symulacja wysłania przypomnienia o głosowaniu
        private void SendVotingReminder()
        {
            Console.WriteLine("Przypomnienie: Za tydzień są wybory prezydenckie! Zapraszamy do głosowania.");
            // Tutaj można dodać logikę faktycznego wysłania przypomnienia, na przykład przez e-mail lub wiadomość tekstową
        }
    }
}
