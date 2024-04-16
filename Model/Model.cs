using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientLogic;

namespace Model
{
    public class Model
    {
        private LogicAbstractApi electionService;
        public CandidateRepositoryPresentation candidateRepositoryPresentation { get; private set; }
        public ModelConnectionService ModelConnectionService { get; private set; }
        public Model(LogicAbstractApi? electionServiceAbstract)
        {
            electionService = electionServiceAbstract == null ? LogicAbstractApi.Create() : electionServiceAbstract;
            electionService.GetCandidates();
            candidateRepositoryPresentation = new CandidateRepositoryPresentation(electionService.GetCandidates());

            ModelConnectionService = new ModelConnectionService(electionService.GetConnectionService());
        }

        public LogicAbstractApi GetService()
        {
            return this.electionService;
        }

    }
}
