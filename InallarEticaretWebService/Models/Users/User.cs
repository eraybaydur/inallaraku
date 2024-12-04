namespace InallarEticaretWebService.Models.Users
{
    public class User
    {
        public int LogicalRef { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? RegistirationDate { get; set; }
        public string Password { get; set; } = string.Empty;
        public string ResetToken { get; set; } = string.Empty;

    }
}
