using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TouristService : IInternalTouristService, ITouristService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICrudRepository<Person> _personRepository;
        private readonly ITouristRepository _touristRepository;

        public TouristService(IUserRepository userRepository, ICrudRepository<Person> personRepository, ITouristRepository touristRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _touristRepository = touristRepository;
        }

        public Result<API.Dtos.TouristDto> GetById(int id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                Person person = _personRepository.Get(user.Id);
                Tourist tourist = _touristRepository.GetById((int)user.Id);
                return new API.Dtos.TouristDto(user.Id, user.Username, user.Password, API.Dtos.UserRole.Tourist, user.IsActive, person.Name, person.Surname, person.Email, tourist.Interests.Select(i => new API.Dtos.InterestDto((API.Dtos.InterestType)i.Type)).ToList());
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<API.Dtos.TouristDto> UpdateInterests(int id, List<API.Dtos.InterestDto> interests)
        {
            var user = _userRepository.GetById(id);
            var person = _personRepository.Get(user.Id);
            var tourist = _touristRepository.GetById((int)user.Id);

            tourist.UpdateInterests(interests.Select(interest => new Interest((Domain.InterestType)interest.Type)).ToList());

            tourist = _touristRepository.Update(tourist);

            return new API.Dtos.TouristDto(user.Id, user.Username, user.Password, API.Dtos.UserRole.Tourist, user.IsActive, person.Name, person.Surname, person.Email, tourist.Interests.Select(i => new API.Dtos.InterestDto((API.Dtos.InterestType)i.Type)).ToList());
        }
    }
}
