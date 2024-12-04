namespace InallarEticaretWebService.Dtos.User
{
    public class UserDto
    {
        public int LogicalRef { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime RegistirationDate { get; set; }
    }
}
