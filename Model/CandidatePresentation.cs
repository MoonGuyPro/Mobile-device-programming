using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class CandidatePresentation : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VotesNumber { get; set; }

        public CandidatePresentation(ICandidatePerson candidate)
        {
            Id = candidate.Id;
            Name = candidate.Name;
            VotesNumber = candidate.VotesNumber;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
