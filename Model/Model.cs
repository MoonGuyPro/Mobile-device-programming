using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Interfaces;

namespace Model
{
    public class Model
    {
        private IElectionService _electionService;

        public Model()
        {
            _electionService = IElectionService.CreateInstance(null, null);
        }

        public IElectionService GetService()
        {
            return _electionService;
        }

    }
}
