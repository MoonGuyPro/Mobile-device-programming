using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic;
using ServerData;

namespace ServerLogic
{
    public interface ICandidatePerson
    {
        public int Id { get; }
        public string Name { get; }
    }

    public interface ICandidateCollection
    {
        public List<ICandidatePerson> GetCandidates();
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

        public abstract event EventHandler<int> UpdateDaysToElection;
        public abstract Task SendVotingReminderPeriodically();

    }
}
