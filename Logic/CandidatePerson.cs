﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class CandidatePerson : ICandidatePerson
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = "";

        public CandidatePerson(ICandidateModel candidate)
        {
            Id = candidate.Id;
            Name = candidate.Name;
        }
    }
}