using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Repositories;
namespace ClientData
{
    internal class DataApi : DataAbstractApi
    {
		private readonly CandidateRepository candidateRepository;
		private readonly IConnectionService connectionService;

		public DataApi(IConnectionService? connectionService)
		{
			this.connectionService = connectionService ?? new ConnectionService();
			candidateRepository = new CandidateRepository(this.connectionService);
		}

		public override ICandidateRepository GetCandidateRepository()
		{
			return candidateRepository;
		}

		public override IConnectionService GetConnectionService()
		{
			return connectionService;
		}
	}
}
