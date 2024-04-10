using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Interfaces;
using System.Collections.ObjectModel;

namespace Data.Repositories
{
    internal class InMemoryCandidateRepository : CandidateRepositoryAbstract
    {
        private List<CandidateModel> _candidates;

        public InMemoryCandidateRepository()
        {
            _candidates = new List<CandidateModel>();
            // Inicjalizacja kandydatów
            _candidates.Add(new CandidateModel { Id = 1, Name = "Candidate 1" });
            _candidates.Add(new CandidateModel { Id = 2, Name = "Candidate 2" });
            _candidates.Add(new CandidateModel { Id = 3, Name = "Candidate 3" });
            _candidates.Add(new CandidateModel { Id = 4, Name = "Candidate 4" });
            _candidates.Add(new CandidateModel { Id = 5, Name = "Candidate 5" });
        }

        public override List<CandidateModel> GetAllCandidates()
        {
            return _candidates;
        }

        public override CandidateModel GetCandidateById(int id)
        {
            return _candidates.FirstOrDefault(c => c.Id == id);
        }

        public override void AddCandidate(CandidateModel candidate)
        {
            _candidates.Add(candidate);
        }

        public override void UpdateCandidate(CandidateModel candidate)
        {
            var existingCandidate = _candidates.FirstOrDefault(c => c.Id == candidate.Id);
            if (existingCandidate != null)
            {
                // Aktualizacja danych kandydata
                existingCandidate.Name = candidate.Name;
                // inne właściwości
            }
        }

        public override void DeleteCandidate(int id)
        {
            _candidates.RemoveAll(c => c.Id == id);
        }
    }
}
