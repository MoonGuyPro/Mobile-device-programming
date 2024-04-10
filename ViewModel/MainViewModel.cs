using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Model.Model model;
        private ObservableCollection<CandidatePresentation> _candidates;
        private CandidatePresentation _selectedCandidate;
        private int _daysToElection;
        public RelayCommand VoteCommand { get; private set; }

        public CandidatePresentation SelectedCandidate
        {
            get => _selectedCandidate;
            set => _selectedCandidate = value;

        }

        public ObservableCollection<CandidatePresentation> Candidates
        {
            get => _candidates;
            set
            {
                _candidates = value;
                OnPropertyChanged(nameof(Candidates));
            }
        }


        public int DaysToElection
        {
            get => _daysToElection;
            set
            {
                _daysToElection = value;
                OnPropertyChanged(nameof(DaysToElection));
            }
        }

        public MainViewModel()
        {
            this.model = new Model.Model(null);
            Candidates = new ObservableCollection<CandidatePresentation>(model.candidateRepositoryPresentation.GetCandidates());
            VoteCommand = new RelayCommand(VoteForCandidate);
            // Zarejestruj się na zdarzenie UpdateDaysToElection
            model.GetService().UpdateDaysToElection += OnUpdateDaysToElection;
            Task.Run(() => model.GetService().SendVotingReminderPeriodically());
        }

/*        public MainViewModel(IElectionService electionService)
        {
            this.electionService = electionService;
            LoadCandidates();
            VoteCommand = new RelayCommand(VoteForCandidate);
        }*/

        private void LoadCandidates()
        {
            var candidates = model.candidateRepositoryPresentation.GetCandidates();
            Candidates = new ObservableCollection<CandidatePresentation>(candidates);
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

        private void OnUpdateDaysToElection(object sender, int days)
        {
            DaysToElection = days;
        }
        
    }
}
