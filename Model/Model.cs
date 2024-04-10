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
        private ElectionServiceAbstract _electionService;

        public Model()
        {
            //_electionService = ElectionServiceAbstract.CreateInstance(null, null);
           // _electionService.GetAllCandidates();
        }

        public ElectionServiceAbstract GetService()
        {
            return _electionService;
        }

    }
}
