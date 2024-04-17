using ClientApi;
using ServerLogic;
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

			connection.OnMessage = OnMessage;
			connection.OnError = OnError;
			connection.OnClose = OnClose;

			webSocketConnection = connection;
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
/*			else if (serializer.GetCommandHeader(message) == SellItemCommand.StaticHeader)
			{
				SellItemCommand sellItemCommand = serializer.Deserialize<SellItemCommand>(message);

				TransactionResponse transactionResponse = new TransactionResponse();
				transactionResponse.TransactionId = sellItemCommand.TransactionID;
				try
				{
					logicAbstractApi.GetShop().SellItem(sellItemCommand.ItemID);
					transactionResponse.Succeeded = true;
				}
				catch (Exception exception)
				{
					Console.WriteLine($"Exception \"{exception.Message}\" caught during selling item");
					transactionResponse.Succeeded = false;
				}

				string transactionMessage = serializer.Serialize(transactionResponse);
				Console.WriteLine($"Send: {transactionMessage}");
				await webSocketConnection.SendAsync(transactionMessage);
			}*/
		}

		private async Task SendCandidates()
        {
			if (webSocketConnection == null)
				return;

			Console.WriteLine("Candidates sent");
			
			UpdateAllResponce serverResponce = new UpdateAllResponce();
			List<ICandidatePerson> candidates = logicAbstractApi.GetCandidates().GetCandidates();
			serverResponce.candidates = candidates.Select(x => x.ToDTO()).ToArray();

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
