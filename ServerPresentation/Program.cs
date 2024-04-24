using ClientApi;
using ServerLogic;
using ClientData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPresentation
{
    internal class Program
    {
		private readonly LogicAbstractApi logicAbstractApi;

		private WebSocketConnection? webSocketConnection;

		private Program(LogicAbstractApi logicAbstractApi)
		{
			this.logicAbstractApi = logicAbstractApi;
			//logicAbstractApi.GetCandidates().DaysToElectionChanged += OnUpdateDaysToElectionAsync;
		}


		private async Task StartConnection()
		{
			while (true)
			{
				Console.WriteLine("Waiting for connect...");
				await WebSocketServer.StartServer(21370, OnConnect);
			}
		}

		private void OnConnect(WebSocketConnection connection)
		{
			Console.WriteLine($"Connected to {connection}");
			logicAbstractApi.UpdateDaysToElection += OnUpdateDaysToElectionAsync;
			Task.Run(() => logicAbstractApi.SendVotingReminderPeriodically());
			connection.OnMessage = OnMessage;
			connection.OnError = OnError;
			connection.OnClose = OnClose;
			webSocketConnection = connection;

		}
		/*		private void OnUpdateDaysToElection(object sender, int days)
				{
					DaysToElection = days;
				}*/
		private void OnUpdateDaysToElectionAsync(object sender, int days)
		{
			if (webSocketConnection == null)
				return;

			Console.WriteLine("Days left sent : " , days);

			VotingReminder serverResponce = new VotingReminder();
			serverResponce.daysToElection = days;

			Serializer serializer = Serializer.Create();
			string responceJson = serializer.Serialize(serverResponce);
			Console.WriteLine(responceJson);
			Task.Run(async () => await SendHelp(responceJson));
		}
		private async Task SendHelp(string responceJson)
		{
			if (webSocketConnection == null)
				return;
			await webSocketConnection.SendAsync(responceJson);
		}

			private async void OnMessage(string message)
		{
			if (webSocketConnection == null)
				return;

			Console.WriteLine($"New message: {message}");

			Serializer serializer = Serializer.Create();
			if (serializer.GetCommandHeader(message) == GetCandidatesCommand.StaticHeader)
			{
				GetCandidatesCommand getCandidatesCommand = serializer.Deserialize<GetCandidatesCommand>(message);
				Task task = Task.Run(async () => await SendCandidates());
			}
			else if (serializer.GetCommandHeader(message) == VoteForCandidateCommand.StaticHeader)
			{
				VoteForCandidateCommand votesForCandidateCommand = serializer.Deserialize<VoteForCandidateCommand>(message);

				//VotingResponce votingResponce = new VotingResponce();
				//votingResponce.id = votesForCandidateCommand.CandidateId;

				//logicAbstractApi.GetCandidates().GetVotesForCandidate(votesForCandidateCommand.CandidateId);
				logicAbstractApi.GetCandidates().AddVote(votesForCandidateCommand.CandidateId);

				//string votingMessage = serializer.Serialize(votingResponce);
				//Console.WriteLine($"Send: {votingMessage}");
				//await webSocketConnection.SendAsync(votingMessage);
			}
		}

/*		private async void HandleDaysChanged(object sender, LogicDaysToElectionChangedEventArgs args)
        {
			if (webSocketConnection == null)
				return;

			Serializer serializer = Serializer.Create();
			string responseJson = serializer.Serialize(inflationChangedResponse);
			Console.WriteLine(responseJson);

			await webSocketConnection.SendAsync(responseJson);
		}*/

		private async Task SendCandidates()
        {
			if (webSocketConnection == null)
				return;

			Console.WriteLine("Candidates sent");
			
			UpdateAllResponce serverResponce = new UpdateAllResponce();
			List<ICandidatePerson> candidates = logicAbstractApi.GetCandidates().GetCandidates();
			serverResponce.Candidates = candidates.Select(x => x.ToDTO()).ToArray();

			Serializer serializer = Serializer.Create();
			string responceJson = serializer.Serialize(serverResponce);
			Console.WriteLine(responceJson);

			await webSocketConnection.SendAsync(responceJson);
        }

		private void OnError()
		{
			Console.WriteLine($"Connection error");
		}

		private async void OnClose()
		{
			Console.WriteLine($"Connection closed");
			webSocketConnection = null;
		}


		private static async Task Main(string[] args)
		{
			Program program = new Program(LogicAbstractApi.Create());
			await program.StartConnection();
		}
	}
}
