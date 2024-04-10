using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Repositories;

namespace Data
{
    public abstract class CandidateRepositoryAbstract
    {
        public abstract List<CandidateModel> GetAllCandidates();
        public abstract CandidateModel GetCandidateById(int id);
        public abstract void AddCandidate(CandidateModel candidate);
        public abstract void UpdateCandidate(CandidateModel candidate);
        public abstract void DeleteCandidate(int id);

        public static CandidateRepositoryAbstract? CreateInstance()
        {
            return new InMemoryCandidateRepository();
        }
    }
}
