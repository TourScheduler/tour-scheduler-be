using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TouristDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<InterestDto> Interests { get; set; }

        public TouristDto(long id, string username, string password, UserRole role, bool isActive, string name, string surname, string email, List<InterestDto> interests)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            IsActive = isActive;
            Name = name;
            Surname = surname;
            Email = email;
            Interests = interests;
        }
    }

    public enum UserRole
    {
        Administrator,
        Author,
        Tourist
    }
}
