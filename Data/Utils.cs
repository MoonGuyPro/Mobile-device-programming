using ClientApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientData
{
    internal static class Utils
    {
		public static CandidateModel ItemTypeFromString(string typeAsString)
		{
			return (CandidateModel)Enum.Parse(typeof(CandidateModel), typeAsString);
		}

		public static string ToString(this CandidateModel typeAsString)
		{
			return Enum.GetName(typeof(CandidateModel), typeAsString) ?? throw new InvalidOperationException();
		}

		public static ICandidateModel ToCandidate(this CandidateDTO candidateDTO)
		{
			return new CandidateModel(
				candidateDTO.Id,
				candidateDTO.Name
			);
		}
	}
}
