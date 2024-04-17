using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
	public interface ICandidateModel
	{
		int Id { get; set; }
		string Name { get; set; }
		int VotesNumber { get; set; }
	}

	public interface ICandidateRepository
	{
		public void AddCandidate(int id, string name);
		public void RemoveCandidate(int id);
		public List<ICandidateModel> GetAllCandidates();
		public int GetVotesNumberForCandidate(int id);
		public void AddVote(int id);
	}

	public abstract class DataAbstractApi
	{
		public static DataAbstractApi Create()
		{
			return new DataApi();
		}

		public abstract ICandidateRepository GetCandidateRepository();
	}
}
