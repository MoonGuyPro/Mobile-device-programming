using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApi;
using ServerLogic;


namespace ServerPresentation
{
    internal static class Utils
    {
        public static CandidateDTO ToDTO(this ICandidatePerson candidate)
        {
            return new CandidateDTO(candidate.Id, candidate.Name, candidate.VotesNumber);
        }
    }
}
