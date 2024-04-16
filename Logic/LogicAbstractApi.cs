﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientData;

namespace ClientLogic
{
    public interface ICandidatePerson
    {
        public int Id { get;}
        public string Name { get;}
        public int VotesNumber { get;}
    }

    public interface ICandidateCollection
    {
        public List<ICandidatePerson> GetCandidates();
        public int GetVotesForCandidate(int id);
    }

    public interface ILogicConnectionService
    {
        public event Action<string>? Logger;
        public event Action? OnConnectionStateChanged;

        public event Action<string>? OnMessage;
        public event Action? OnError;

        public Task Connect(Uri peerUri);
        public Task Disconnect();

        public bool IsConnected();
    }

    public abstract class LogicAbstractApi
    {
        public DataAbstractApi DataApi { get; set; }

        public LogicAbstractApi(DataAbstractApi dataAbstractApi)
        {
            DataApi = dataAbstractApi;
        }

        public static LogicAbstractApi Create(DataAbstractApi? candidateRepositoryAbstract = null) 
        {
            DataAbstractApi candidateRepository = candidateRepositoryAbstract ?? DataAbstractApi.Create(null);
            return new LogicApi(candidateRepository);
        }

        public abstract ICandidateCollection GetCandidates();
        public abstract int GetVotesForCandidate(int id);

        public abstract event EventHandler<int> UpdateDaysToElection;
        public abstract Task SendVotingReminderPeriodically();
        public abstract ILogicConnectionService GetConnectionService();
    }
}
