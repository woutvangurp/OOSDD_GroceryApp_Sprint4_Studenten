using Grocery.Core.Models.Enums;

namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Enums.Enums.Role UserRole { get; set; }
        public Client(int id, string name, string emailAddress, string password, Enums.Enums.Role role) : base(id, name)
        {
            EmailAddress = emailAddress;
            Password = password;
            UserRole = role;
        }
    }
}
