using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInternalTourProblemService _tourProblemService;

        public UserService(IUserRepository userRepository, IInternalTourProblemService tourProblemService)
        {
            _userRepository = userRepository;
            _tourProblemService = tourProblemService;
        }
        public Result<UserDto> Block(int id)
        {
            try
            {
                var user = _userRepository.GetById(id);

                user.Block();
                user = _userRepository.Update(user);

                return new UserDto(user.Id, user.Username, user.Password, (API.Dtos.UserRole)user.Role, user.IsActive);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                // There is a subtle issue here. Can you find it?
            }
        }

        public Result<List<UserDto>> FindMaliciousUsers()
        {
            List<UserDto> users = new List<UserDto>();
            foreach (var user in _userRepository.GetTourists())
            {
                if (_tourProblemService.CheckMaliciousTourist((int)user.Id))
                {
                    users.Add(new UserDto(user.Id, user.Username, user.Password, (API.Dtos.UserRole)user.Role, user.IsActive));
                }
            }

            return users;
        }
    }
}
