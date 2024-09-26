namespace MbaBlog.Util.Users.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public required string UserEmail { get; set; }
    }
}
