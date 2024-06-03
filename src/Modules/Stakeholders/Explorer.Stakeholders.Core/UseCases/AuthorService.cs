﻿using AutoMapper;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AuthorService : IAuthorService
    {
        private readonly IServiceScope _scope;
        private readonly IAuthorRepository _authorRepository;
        private readonly IInternalReportService _reportService;

        public AuthorService(IServiceScopeFactory serviceScopeFactory)
        {
            _scope = serviceScopeFactory.CreateScope();
            _authorRepository = _scope.ServiceProvider.GetRequiredService<IAuthorRepository>();
            _reportService = _scope.ServiceProvider.GetRequiredService<IInternalReportService>();
        }

        public void Award()
        {
            long authorId = _reportService.FindAuthorByMostSoldedTours();
            Author author = _authorRepository.GetByUserId(authorId);
            author.AddPoints();
            author.Award();
            author = _authorRepository.Update(author);
        }
    }
}
