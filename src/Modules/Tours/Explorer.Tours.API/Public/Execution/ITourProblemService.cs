﻿using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Execution
{
    public interface ITourProblemService
    {
        Result<TourProblemDto> Create(CreateTourProblemDto createTourProblem);
    }
}
