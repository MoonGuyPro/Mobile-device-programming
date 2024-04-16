using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data;
using Data.Repositories;
using Logic.Services;

namespace Logic
{
    public interface ICandidatePerson
    {
        public int Id { get;}
        public string Name { get;}
        public int VotesNumber { get;}
    }

    public interface ICandidateCollection
    {
        public List<ICandidatePerson> GetCandidates();
        public int GetVotesForCandidate(int id);
    }

    public abstract class ElectionServiceAbstract
    {
        public CandidateRepositoryAbstract CandidateApi { get; set; }

        public ElectionServiceAbstract(CandidateRepositoryAbstract candidateRepositoryAbstract)
        {
            CandidateApi = candidateRepositoryAbstract;
        }

        public static ElectionServiceAbstract Create(CandidateRepositoryAbstract? candidateRepositoryAbstract = null) 
        {
            CandidateRepositoryAbstract candidateRepository = candidateRepositoryAbstract ?? CandidateRepositoryAbstract.Create();
            return new ElectionService(candidateRepository);
        }

        public abstract ICandidateCollection GetCandidates();
        public abstract int GetVotesForCandidate(int id);

        public abstract event EventHandler<int> UpdateDaysToElection;
        public abstract Task SendVotingReminderPeriodically();

    }
}
