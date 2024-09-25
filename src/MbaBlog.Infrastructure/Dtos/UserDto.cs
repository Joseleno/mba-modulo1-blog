using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Dtos
{
    public class UserDto
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
    }
}
