using System;

namespace ClientApi
{
	[Serializable]
	public abstract class ServerCommand
	{
		public string Header;

		protected ServerCommand(string header)
		{
			Header = header;
		}
	}
	[Serializable]
	public struct CandidateDTO
	{
		public int Id;
		public string Name;

		public CandidateDTO(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}

	[Serializable]
	public abstract class ServerResponse
	{
		public string Header { get; private set; }

		protected ServerResponse(string header)
		{
			Header = header;
		}
	}
}
