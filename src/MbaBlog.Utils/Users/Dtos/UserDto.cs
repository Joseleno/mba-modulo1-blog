using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Utils.Users.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public required string UserEmail { get; set; }
    }
}
