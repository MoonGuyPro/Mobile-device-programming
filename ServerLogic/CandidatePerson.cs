﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;

namespace ServerLogic
{
    internal class CandidatePerson : ICandidatePerson
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = "";

        public int VotesNumber { get; private set; }

        public CandidatePerson(ICandidateModel candidate)
        {
            Id = candidate.Id;
            Name = candidate.Name;
            VotesNumber = candidate.VotesNumber;
        }
    }
}
