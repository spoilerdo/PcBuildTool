using KillerApp.Domain;

namespace KillerApp.Models
{
    public class CreateAccountViewModel
    {
        public bool UsernameRight { get; set; }
        public bool PasswordRight { get; set; }
        public Account Account { get; set; }
    }
}