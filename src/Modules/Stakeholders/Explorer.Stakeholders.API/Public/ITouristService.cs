﻿using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface ITouristService
    {
        Result<TouristDto> GetById(int id);
        Result<TouristDto> UpdateInterests(int id, List<InterestDto> interests);
    }
}
