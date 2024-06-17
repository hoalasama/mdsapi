namespace mdswebapi.Dtos.Account
{
    public class RoleUpdateDto
    {
        public string UserId { get; set; }
        public string NewRole { get; set; } = string.Empty;
    }
}
