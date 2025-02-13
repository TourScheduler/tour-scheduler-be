﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IReportRepository
    {
        Report Create(Report report);
        Report FindReportByAuthorAndDate(int month, int year, long authorId);
        List<Report> GetByAuthorId(int authorId);
        List<Report> GetByDate(int month, int year);
    }
}
