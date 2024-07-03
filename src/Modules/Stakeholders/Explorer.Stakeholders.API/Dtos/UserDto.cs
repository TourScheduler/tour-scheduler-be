using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }

        public UserDto(long id, string username, string password, UserRole role, bool isActive)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            IsActive = isActive;
        }
    }
}
