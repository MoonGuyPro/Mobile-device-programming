using System;

namespace ClientApi
{
	[Serializable]
	public abstract class ServerCommand
	{
		public string Header { get; set; }

		protected ServerCommand(string header)
		{
			Header = header;
		}
	}

	[Serializable]
	public class GetCandidatesCommand : ServerCommand
	{
		public static string StaticHeader = "GetCandidates";

		public GetCandidatesCommand()
		: base(StaticHeader)
		{

		}
	}

	[Serializable]
	public struct CandidateDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Votes { get; set; }

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

	[Serializable]
	public class VoteForCandidateCommand : ServerCommand
    {
		public static string StaticHeader = "VoteForCandidate";

		public int CandidateId;

		public VoteForCandidateCommand(int candidateId) : base(StaticHeader) { CandidateId = candidateId; }
	}

	[Serializable]
	public class VotingResponce : ServerResponse
    {
		public static readonly string StaticHeader = "VotingResponce";

		public int id { get; set; }

		public VotingResponce() : base(StaticHeader) { }
    }

	public class VotingReminder : ServerResponse
    {
		public static readonly string StaticHeader = "VoringReminder";

		public int daysToElection { get; set; }

		public VotingReminder() : base(StaticHeader) { }


    }


}
