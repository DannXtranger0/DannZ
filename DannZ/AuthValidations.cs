using DannZ.Context;

namespace DannZ
{
    public class AuthValidations
    {
        private readonly MyDbContext _context;
        public AuthValidations(MyDbContext context)
        {
            _context = context;
        }
        public  bool ValidateEmail(string email)
        {
            return _context.Users.Any(u=> u.Email.Equals(email));
        }


    }
}
