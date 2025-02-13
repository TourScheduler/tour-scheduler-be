﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ITouristRepository
    {
        Tourist Create(Tourist tourist);
        Tourist GetById(int id);
        Tourist Update(Tourist tourist);
    }
}
