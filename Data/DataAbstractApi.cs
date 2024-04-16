using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientData
{

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
}
