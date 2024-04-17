using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    internal class DataApi : DataAbstractApi
    {
		private readonly CandidateRepository candidateRepository;

		public DataApi()
		{
			candidateRepository = new CandidateRepository();
		}

		public override ICandidateRepository GetCandidateRepository()
		{
			return candidateRepository;
		}

	}
}
