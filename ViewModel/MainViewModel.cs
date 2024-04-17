﻿using System;
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
        private string connectionString;
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
            Candidates = new ObservableCollection<CandidatePresentation>(model.candidateRepositoryPresentation.GetCandidates());
            VoteCommand = new RelayCommand(VoteForCandidate);

            OnConnectionStateChanged();

            // Zarejestruj się na zdarzenie UpdateDaysToElection
            model.GetService().UpdateDaysToElection += OnUpdateDaysToElection;
            Task.Run(() => model.GetService().SendVotingReminderPeriodically());
        }

        private void LoadCandidates()
        {
            var candidates = model.candidateRepositoryPresentation.GetCandidates();
            Candidates = new ObservableCollection<CandidatePresentation>(candidates);
        }

        private void VoteForCandidate(object candidateId)
        {
           if (_selectedCandidate != null)
             {
                _selectedCandidate.VotesNumber++;
                 //System.Diagnostics.Debug.WriteLine($"Voted for candidate ID: {_selectedCandidate.Id}");
             }
/*             else
             {
                 System.Diagnostics.Debug.WriteLine("No candidate selected.");
             }*/

        }

        private void OnUpdateDaysToElection(object sender, int days)
        {
            DaysToElection = days;
        }
        private void OnConnectionStateChanged()
        {
            bool actualState = model.ModelConnectionService.IsConnected();
            ConnectionString = actualState ? "Connected" : "Disconnected";

            if (!actualState)
            {
                Task.Run(() => model.ModelConnectionService.Connect(new Uri(@"ws://localhost:21370")));
            }
            else
            {
                //model.WarehousePresentation.RequestUpdate();
            }
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
