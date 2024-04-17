using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;

namespace ClientLogic
{
    internal class LogicApi : LogicAbstractApi
    {
        private readonly ICandidateCollection _candidates;

        public LogicApi(DataAbstractApi dataAbstractApi) : base(dataAbstractApi)
        {
            _candidates = new CandidateCollection(dataAbstractApi.GetCandidateRepository());
            
        }

        public override ICandidateCollection GetCandidates()
        {
            return _candidates;
        }

        public override event EventHandler<int> UpdateDaysToElection;

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

        public override int GetVotesForCandidate(int id)
        {
            int votes = _candidates.GetVotesForCandidate(id);
            return votes;
        }
    }
}
