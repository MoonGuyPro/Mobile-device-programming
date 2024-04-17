using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;
using System.Collections.ObjectModel;
using ClientApi;

namespace ClientData
{
    internal class CandidateRepository : ICandidateRepository
    {
        private List<ICandidateModel> _candidates = new List<ICandidateModel>();

        private readonly IConnectionService connectionService;

        public event Action? CandidatesUpdate;

        public int daysToElection = 22;

        public CandidateRepository(IConnectionService connectionService)
        {
            AddCandidate(1, "Candidate 1");
            AddCandidate(2, "Candidate 2");
            AddCandidate(3, "Candidate 3");
            AddCandidate(4, "Candidate 4");
            AddCandidate(5, "Candidate 5");


            this.connectionService = connectionService;
            this.connectionService.OnMessage += OnMessage;
        }

        private void OnMessage(string message)
        {
            Serializer serializer = Serializer.Create();
            if (serializer.GetResponseHeader(message) == UpdateAllResponce.StaticHeader)
            {
                UpdateAllResponce responce = serializer.Deserialize<UpdateAllResponce>(message);
                UpdateAllCandidates(responce);
            } else if (serializer.GetResponseHeader(message) == VotingReminder.StaticHeader)
            {
                VotingReminder responce = serializer.Deserialize<VotingReminder>(message);
                DaysToElection(responce);
            }
            else 
            {
                Task.Run(() => RequestCandidates());
            }

        }

        private void DaysToElection(VotingReminder responce)
        {
            if (responce.daysToElection == null)
                return;

            daysToElection = responce.daysToElection;
        }

        private void UpdateAllCandidates(UpdateAllResponce responce)
        {
            if (responce.candidates == null)
                return;

            _candidates.Clear();
            foreach (CandidateDTO candidate in responce.candidates)
            {
                _candidates.Add(candidate.ToCandidate());
            }
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

        public int getDays()
        {
            return daysToElection;
        }
        public async Task RequestCandidates()
        {
            Serializer serializer = Serializer.Create();
            await connectionService.SendAsync(serializer.Serialize(new GetCandidatesCommand()));
        }

        public void RequestUpdate()
        {
            if (connectionService.IsConnected())
            {
                Task task = Task.Run(async () => await RequestCandidates());
            }
        }

        public async Task VoteForCandidate(int id)
        {
            if (connectionService.IsConnected())
            {
                Serializer serializer = Serializer.Create();
                VoteForCandidateCommand voteForCandidateCommand = new VoteForCandidateCommand(id);
                await connectionService.SendAsync(serializer.Serialize(voteForCandidateCommand));   

            }
        }

    }
}
