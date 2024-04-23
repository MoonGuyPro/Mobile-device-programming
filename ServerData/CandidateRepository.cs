using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ServerData
{
    internal class CandidateRepository : ICandidateRepository
    {
        private List<CandidateModel> _candidates = new List<CandidateModel>();
        private object candidatesLock = new object();
        private object electionLock = new object();
        private object votesLock = new object();

        public event EventHandler<DaysToElectionChangedEventArgs>? DaysToElectionChanged;

        public CandidateRepository()
        {
            AddCandidate(1, "Candidate From Server 1");
            AddCandidate(2, "Candidate 2");
            AddCandidate(3, "Candidate 3");
            AddCandidate(4, "Candidate 4");
            AddCandidate(5, "Candidate 5");

            //this.connectionService.OnMessage += OnMessage;
        }


        public async void SendVotingReminderPeriodically()
        {
            while (true)
            {
                DateTime electionDay = new DateTime(2024, 05, 10); // Przykładowa data wyborów
                TimeSpan timeRemaining = electionDay - DateTime.Today; // Czas pozostały do wyborów
                //System.Diagnostics.Debug.WriteLine(timeRemaining);

                // Aktualizacja liczby dni pozostałych do wyborów w MainViewModel
                //UpdateDaysToElection?.Invoke(this, timeRemaining.Days);
                DaysToElectionChanged?.Invoke(this, new DaysToElectionChangedEventArgs(timeRemaining.Days));

                // Poczekaj jeden dzień
                await Task.Delay(TimeSpan.FromDays(1)); // Sprawdź co dzień
            }
        }

        public void AddCandidate(int id, string name)
        {
            lock (candidatesLock)
            {
                _candidates.Add(new CandidateModel(id, name));
            }
            
        }

        public List<ICandidateModel> GetAllCandidates()
        {
            
            List<ICandidateModel> candidates = new List<ICandidateModel>();
            lock (candidatesLock)
            {
                candidates.AddRange(_candidates);
            }
            

            return candidates;
        }

        public void RemoveCandidate(int id)
        {
            lock (candidatesLock)
            {
                _candidates.RemoveAt(id - 1);
            }
            
        }

        public int GetVotesNumberForCandidate(int id)
        {
            lock (votesLock)
            {
                foreach (CandidateModel candidate in _candidates)
                {
                    if (candidate.Id == id)
                        return candidate.VotesNumber;
                }
                return 0;
            }

        }

        public void AddVote(int id)
        {
            lock (votesLock)
            {
                foreach (CandidateModel candidate in _candidates)
                {
                    if (candidate.Id == id)
                        candidate.VotesNumber++;
                }
            }

        }


    }
}
