namespace Services.Models.User
{
    public class LoginUserDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string JwtToken { get; set; }
    }
}
