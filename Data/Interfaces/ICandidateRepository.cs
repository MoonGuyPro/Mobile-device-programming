using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Interfaces
{
    public interface ICandidateRepository
    {
        List<CandidateModel> GetAllCandidates();
        CandidateModel GetCandidateById(int id);
        void AddCandidate(CandidateModel candidate);
        void UpdateCandidate(CandidateModel candidate);
        void DeleteCandidate(int id);
    }
}
