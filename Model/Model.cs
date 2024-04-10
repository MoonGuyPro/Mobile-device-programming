using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class Model
    {
        private ElectionServiceAbstract electionService;

        public CandidateRepositoryPresentation candidateRepositoryPresentation { get; private set; }

        public Model(ElectionServiceAbstract? electionServiceAbstract)
        {
            this.electionService = electionServiceAbstract == null ? ElectionServiceAbstract.Create() : electionServiceAbstract;
            this.electionService.GetCandidates();
            this.candidateRepositoryPresentation = new CandidateRepositoryPresentation(this.electionService.GetCandidates());
        }

        public ElectionServiceAbstract GetService()
        {
            return this.electionService;
        }

    }
}
