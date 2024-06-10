using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserInfoDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname {  get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }

        public UserInfoDto(long id, string name, string surname, string username, UserRole role, bool isActive )        {
            Id = id;
            Name = name;
            Surname = surname;
            Username = username;
            Role = role;
            IsActive = isActive;
        }

    }
}
