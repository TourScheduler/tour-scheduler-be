using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAuthorService
    {
        void Award();
        Result<AuthorDto> GetByUserId(long userId);
        Result<List<AuthorDto>> GetAll();
    }
}
