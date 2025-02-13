﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Internal
{
    public interface IInternalTourProblemService
    {
        public bool CheckMaliciousTourist(int touristId);
        public bool CheckMaliciousAuthor(int authorId);
    }
}
