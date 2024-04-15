using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    internal class VoteModel : IVoteModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }

        public VoteModel(int id, int candidateId)
        {
            Id = id;
            CandidateId = candidateId;
        }
    }
}
