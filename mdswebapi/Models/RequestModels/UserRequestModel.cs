namespace mdswebapi.Models.RequestModels
{
    public sealed record UserRequestModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Roles {  get; set; } = string.Empty;
    }
}
