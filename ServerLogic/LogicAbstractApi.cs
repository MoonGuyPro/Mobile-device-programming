﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;

namespace ServerLogic
{
    public interface ICandidatePerson
    {
        public int Id { get;}
        public string Name { get;}
        public int VotesNumber { get;}
    }

    public interface ICandidateCollection
    {
        public event EventHandler<LogicDaysToElectionChangedEventArgs> DaysToElectionChanged;
        public List<ICandidatePerson> GetCandidates();
        public int GetVotesForCandidate(int id);

        public void AddVote(int id);   
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
            DataAbstractApi candidateRepository = candidateRepositoryAbstract ?? DataAbstractApi.Create();
            return new LogicApi(candidateRepository);
        }

        public abstract ICandidateCollection GetCandidates();
        public abstract int GetVotesForCandidate(int id);

        public abstract event EventHandler<int> UpdateDaysToElection;
        public abstract Task SendVotingReminderPeriodically();


    }
    public class LogicDaysToElectionChangedEventArgs : EventArgs
    {
        public int DaysToElection { get; }

        public LogicDaysToElectionChangedEventArgs(int daysToElection)
        {
            DaysToElection = daysToElection;
        }

        internal LogicDaysToElectionChangedEventArgs(DaysToElectionChangedEventArgs args)
        {
            this.DaysToElection = args.DaysToElection;
        }
    }

}
