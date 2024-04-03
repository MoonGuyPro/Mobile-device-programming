using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Interfaces;

namespace Data.Repositories
{
    public class InMemoryCandidateRepository : ICandidateRepository
    {
        private List<CandidateModel> _candidates;

        public InMemoryCandidateRepository()
        {
            _candidates = new List<CandidateModel>();
            // Inicjalizacja kandydatów
        }

        public List<CandidateModel> GetAllCandidates()
        {
            return _candidates;
        }

        public CandidateModel GetCandidateById(int id)
        {
            return _candidates.FirstOrDefault(c => c.Id == id);
        }

        public void AddCandidate(CandidateModel candidate)
        {
            _candidates.Add(candidate);
        }

        public void UpdateCandidate(CandidateModel candidate)
        {
            var existingCandidate = _candidates.FirstOrDefault(c => c.Id == candidate.Id);
            if (existingCandidate != null)
            {
                // Aktualizacja danych kandydata
                existingCandidate.Name = candidate.Name;
                // inne właściwości
            }
        }

        public void DeleteCandidate(int id)
        {
            _candidates.RemoveAll(c => c.Id == id);
        }
    }
}
