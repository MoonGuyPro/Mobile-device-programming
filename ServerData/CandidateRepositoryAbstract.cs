using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;

namespace ServerData
{


        public interface ICandidateModel
        {
            int Id { get; set; }
            string Name { get; set; }
        }

        public interface ICandidateRepository
        {
            public void AddCandidate(int id, string name);
            public void RemoveCandidate(int id);
            public List<ICandidateModel> GetAllCandidates();
        }

        public abstract class CandidateRepositoryAbstract
        {
            public static CandidateRepositoryAbstract Create()
            {
                return new CandidateApi();
            }

            public abstract ICandidateRepository GetCandidateRepository();
        }
    
}
