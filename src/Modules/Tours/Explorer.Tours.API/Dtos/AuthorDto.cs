using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class AuthorDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Points { get; set; }
        public bool IsAwarded { get; set; }

        public AuthorDto(long id, long userId, int points, bool isAwarded)
        {
            Id = id;
            UserId = userId;
            Points = points;
            IsAwarded = isAwarded;
        }
    }
}
