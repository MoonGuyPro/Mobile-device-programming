using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        // inne właściwości głosu, np. data oddania
    }
}
