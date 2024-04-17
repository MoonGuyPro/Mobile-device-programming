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
	public class GetCandidatesCommand : ServerCommand
	{
		public static string StaticHeader = "GetItems";

		public GetCandidatesCommand()
		: base(StaticHeader)
		{

		}
	}

	[Serializable]
	public struct CandidateDTO
	{
		public int Id;
		public string Name;
		public int Votes;

		public CandidateDTO(int id, string name, int votes)
		{
			Id = id;
			Name = name;
			Votes = votes;
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

	[Serializable]
	public class UpdateAllResponce : ServerResponse
    {
		public static readonly string StaticHeader = "UpdateAllCandidates";

		public CandidateDTO[]? candidates;

		public UpdateAllResponce() : base(StaticHeader) { }
    }

/*	[Serializable]
	public class VoteForCandidate : ServerCommand
    {
		public static string StaticHeader = "VoteForCandidate";



	}*/
}
