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

        public CandidateRepository()
        {
            AddCandidate(1, "Candidate 1");
            AddCandidate(2, "Candidate 2");
            AddCandidate(3, "Candidate 3");
            AddCandidate(4, "Candidate 4");
            AddCandidate(5, "Candidate 5");

            //this.connectionService.OnMessage += OnMessage;
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
