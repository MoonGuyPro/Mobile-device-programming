using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CandidateModel : ICandidateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CandidateModel(int id, string name) 
        {
            Id = id;
            Name = name;    
        }
    }
}
