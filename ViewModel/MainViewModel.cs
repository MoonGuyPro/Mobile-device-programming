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
        //private ObservableCollection<CandidatePresentation> _candidates;
        private AsyncObservableCollection<CandidatePresentation> _candidates;
        private CandidatePresentation _selectedCandidate;
        private int _daysToElection;
        private string connectionString;
        public RelayCommand VoteCommand { get; private set; }

        public CandidatePresentation SelectedCandidate
        {
            get => _selectedCandidate;
            set => _selectedCandidate = value;

        }

        public AsyncObservableCollection<CandidatePresentation> Candidates
        {
            get => _candidates;
            set
            {
                _candidates = value;
                OnPropertyChanged(nameof(Candidates));
            }
        }

        public string ConnectionString
        {
            get => connectionString;
            private set
            {
                if (connectionString != value)
                {
                    connectionString = value;
                    OnPropertyChanged(nameof(connectionString));
                }
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
            model.ModelConnectionService.OnConnectionStateChanged += OnConnectionStateChanged;

            model.ModelConnectionService.Logger += Log;
            model.ModelConnectionService.OnMessage += OnMessage;
            model.candidateRepositoryPresentation.OnCandidatesUpdated += OnCandidatesUpdated;
            model.candidateRepositoryPresentation.DaysToElectionChanged += LoadDays;
            Candidates = new AsyncObservableCollection<CandidatePresentation>(model.candidateRepositoryPresentation.GetCandidates());
            VoteCommand = new RelayCommand(VoteForCandidate);
            OnConnectionStateChanged();

        }
        private void OnCandidatesUpdated()
        {
            Candidates = new AsyncObservableCollection<CandidatePresentation>(model.candidateRepositoryPresentation.GetCandidates());
            //System.Diagnostics.Debug.WriteLine($"cands");
        }

        private void VoteForCandidate(object candidateId)
        {
           if (_selectedCandidate != null)
           {
               //model.candidateRepositoryPresentation.AddVote(_selectedCandidate.Id);
               Task.Run(async () => await model.VoteForCandidate(_selectedCandidate.Id));
               //Candidates = new ObservableCollection<CandidatePresentation>(model.candidateRepositoryPresentation.GetCandidates());
               model.candidateRepositoryPresentation.RequestUpdate();
               //Task.Run(async () => await CandidatesWait());
               //Candidates = new ObservableCollection<CandidatePresentation>(model.candidateRepositoryPresentation.GetCandidates());
           }

        }
        private void LoadDays(object sender, ModelDaysToElectionChangedEventArgs args)
        {
            DaysToElection = model.candidateRepositoryPresentation.getDays();
            //System.Diagnostics.Debug.WriteLine($"days loaded");
        }

        private void OnConnectionStateChanged()
        {
            bool actualState = model.ModelConnectionService.IsConnected();
            ConnectionString = actualState ? "Connected" : "Disconnected";

            if (!actualState)
            {
                Task.Run(() => model.ModelConnectionService.Connect(new Uri(@"ws://localhost:21370")));
                model.candidateRepositoryPresentation.RequestUpdate();
            }
            else
            {
                model.candidateRepositoryPresentation.RequestUpdate();
            }
        }

        private void OnUpdateDaysToElection(object sender, int days)
        {
            DaysToElection = days;
        }
        public async Task CloseConnection()
        {
            if (model.ModelConnectionService.IsConnected())
            {
                await model.ModelConnectionService.Disconnect();
            }
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private void OnMessage(string message)
        {
            Log($"New Message: {message}");
        }

    }
}
