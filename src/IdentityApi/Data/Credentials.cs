using IdentityApi.Models;

namespace IdentityApi.Data
{
    public class Credentials
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Credentials(NewUserDto newUserDro)
        {
            UserName = newUserDro.UserName;
            Password = newUserDro.Password;
        }
        public Credentials() { }
    }
}
