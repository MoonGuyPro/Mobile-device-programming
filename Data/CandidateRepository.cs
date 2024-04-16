using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;
using System.Collections.ObjectModel;

namespace ClientData
{
    internal class CandidateRepository : ICandidateRepository
    {
        private List<CandidateModel> _candidates = new List<CandidateModel>();

        private readonly IConnectionService connectionService;

        public CandidateRepository(IConnectionService connectionService)
        {
            AddCandidate(1, "Candidate 1");
            AddCandidate(2, "Candidate 2");
            AddCandidate(3, "Candidate 3");
            AddCandidate(4, "Candidate 4");
            AddCandidate(5, "Candidate 5");

            //observers = new HashSet<IObserver<InflationChangedEventArgs>>();
            this.connectionService = connectionService;
            //this.connectionService.OnMessage += OnMessage;
        }

        public void AddCandidate(int id, string name)
        {
            _candidates.Add(new CandidateModel(id, name));
        }

        public List<ICandidateModel> GetAllCandidates()
        {
            List<ICandidateModel> candidates = new List<ICandidateModel>();

            candidates.AddRange(_candidates);

            return candidates;
        }

        public void RemoveCandidate(int id)
        {
            _candidates.RemoveAt(id-1);
        }

        public int GetVotesNumberForCandidate(int id)
        {
            foreach(CandidateModel candidate in _candidates)
            {
                if(candidate.Id == id)
                    return candidate.VotesNumber;
            }
            return 0;
        }

        public void AddVote(int id)
        {
            foreach (CandidateModel candidate in _candidates)
            {
                if (candidate.Id == id)
                    candidate.VotesNumber++;
            }
        }
    }
}
