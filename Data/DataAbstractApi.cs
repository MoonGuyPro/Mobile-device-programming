using ClientData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientData
{
	public interface ICandidateModel
	{
		int Id { get; set; }
		string Name { get; set; }
		int VotesNumber { get; set; }
	}

	public interface ICandidateRepository : IObservable<DaysToElectionChangedEventArgs>
	{
		public event Action? CandidatesUpdated;
		public Task VoteForCandidate(int id);
		public void AddCandidate(int id, string name);
		public void RemoveCandidate(int id);
		public void RequestUpdate();
		public List<ICandidateModel> GetAllCandidates();
		public int GetVotesNumberForCandidate(int id);
		public void AddVote(int id);

		public int getDays();
	}

	public interface IConnectionService
	{
		public event Action<string>? Logger;
		public event Action? OnConnectionStateChanged;

		public event Action<string>? OnMessage;
		public event Action? OnError;
		public event Action? OnDisconnect;


		public Task Connect(Uri peerUri);
		public Task Disconnect();

		public bool IsConnected();

		public Task SendAsync(string message);
	}
	public abstract class DataAbstractApi
	{
		public static DataAbstractApi Create(IConnectionService? connectionService)
		{
			return new DataApi(connectionService);
		}

		public abstract ICandidateRepository GetCandidateRepository();
		public abstract IConnectionService GetConnectionService();
	}

	public class DaysToElectionChangedEventArgs : EventArgs
    {
		public int DaysToElection { get; }

		public DaysToElectionChangedEventArgs(int daysToElection)
        {
			DaysToElection = daysToElection;	
        }
    }
}
