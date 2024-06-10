using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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

                SendConfirmationEmail(_userRepository.GetPersonById(user.Id).Email);

                return new UserDto(user.Id, user.Username, user.Password, (API.Dtos.UserRole)user.Role, user.IsActive);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                // There is a subtle issue here. Can you find it?
            }
        }

        public Result<List<UserInfoDto>> FindMaliciousUsers()
        {
            List<UserInfoDto> users = new List<UserInfoDto>();

            FindMaliciousTourists(users);
            FindMaliciousAuthors(users);

            return users;
        }

        public void FindMaliciousAuthors(List<UserInfoDto> users)
        {
            foreach (var user in _userRepository.GetAuthors())
            {
                if (_tourProblemService.CheckMaliciousAuthor((int)user.Id))
                {
                    var person = _userRepository.GetPersonById(user.Id);
                    users.Add(new UserInfoDto(user.Id, person.Name, person.Surname, user.Username, (API.Dtos.UserRole)user.Role, user.IsActive));
                }
            }
        }

        public void FindMaliciousTourists(List<UserInfoDto> users)
        {
            foreach (var user in _userRepository.GetTourists())
            {
                if (_tourProblemService.CheckMaliciousTourist((int)user.Id))
                {
                    var person = _userRepository.GetPersonById(user.Id);
                    users.Add(new UserInfoDto(user.Id, person.Name, person.Surname, user.Username, (API.Dtos.UserRole)user.Role, user.IsActive));
                }
            }
        }

        public void SendConfirmationEmail(string receiverEmail)
        {
            string senderEmail = "isaprojekat1@gmail.com";
            string senderPassword = "bpyjrqpswdubdjrf";

            string recipientEmail = receiverEmail;

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                var message = new MailMessage(senderEmail, recipientEmail);
                message.Subject = "Blocking";
                message.Body = "You are blocked, so you cannot login into application.";

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }

        public Result<UserDto> Unblock(int id)
        {
            try
            {
                var user = _userRepository.GetById(id);

                user.Unblock();
                user = _userRepository.Update(user);

                return new UserDto(user.Id, user.Username, user.Password, (API.Dtos.UserRole)user.Role, user.IsActive);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                // There is a subtle issue here. Can you find it?
            }
        }
    }
}
