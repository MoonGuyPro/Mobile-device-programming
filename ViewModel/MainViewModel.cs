using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Interfaces;
using Data.Models;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IElectionService electionService;
        private ObservableCollection<CandidateModel> _candidates;
        public RelayCommand VoteCommand { get; private set; }

        private CandidateModel _selectedCandidate;
        public CandidateModel SelectedCandidate
        {
            get => _selectedCandidate;
            set => _selectedCandidate = value;

        }
        public ObservableCollection<CandidateModel> Candidates
        {
            get => _candidates;
            set
            {
                _candidates = value;
                OnPropertyChanged(nameof(Candidates));
            }
        }
        public MainViewModel()
        {
            // Populate with example candidates
            Candidates = new ObservableCollection<CandidateModel>
            {
                new CandidateModel { Id = 1, Name = "Candidate 1" },
                new CandidateModel { Id = 2, Name = "Candidate 2" },
                new CandidateModel { Id = 3, Name = "Candidate 3" },
                new CandidateModel { Id = 4, Name = "Candidate 4" },
                new CandidateModel { Id = 5, Name = "Candidate 5" }
            };
            VoteCommand = new RelayCommand(VoteForCandidate);
        }

        public MainViewModel(IElectionService electionService)
        {
            this.electionService = electionService;
            LoadCandidates();
        }

        private void LoadCandidates()
        {
            var candidates = electionService.GetAllCandidates();
            Candidates = new ObservableCollection<CandidateModel>(candidates);
        }

        private void VoteForCandidate(object candidateId)
        {
             if (_selectedCandidate != null)
             {
                 System.Diagnostics.Debug.WriteLine($"Voted for candidate ID: {_selectedCandidate.Id}");
             }
             else
             {
                 System.Diagnostics.Debug.WriteLine("No candidate selected.");
             }

        }


    }
}
