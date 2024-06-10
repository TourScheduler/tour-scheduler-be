using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserService
    {
        Result<UserDto> Block(int id);
        Result<List<UserDto>> FindMaliciousUsers();
        void FindMaliciousAuthors(List<UserDto> users);
        void FindMaliciousTourists(List<UserDto> users);
    }
}
