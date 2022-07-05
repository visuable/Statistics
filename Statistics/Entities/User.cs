using Statistics.Helpers;

namespace Statistics.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Session> Sessions { get; set; }

        public class Builder
        {
            private readonly User _user = new ();
            public Builder HasIdentity()
            {
                var identity = IdentityHelper.GenerateNew();
                _user.Login = identity.Login;
                _user.PasswordSalt = identity.PasswordSalt;
                _user.PasswordHash = identity.PasswordHash;
                return this;
            }

            public Builder HasContact(string phone, string email)
            {
                _user.Phone = phone;
                _user.Email = email;
                return this;
            }

            public User Build()
            {
                return _user;
            }
        }
    }
}
