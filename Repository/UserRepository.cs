using APIrestASP_NETudemy.Data.VO;
using APIrestASP_NETudemy.Model;
using Microsoft.EntityFrameworkCore;
using RestASPNETErudio.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace APIrestASP_NETudemy.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly MySQLContext _context;

        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == username));
        }

        public UserRepository(MySQLContext context)
        {
                _context = context;
        }
        public User ValidateCredentials(UserVO user)
        {

            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User RefreshUserInfo(User user)
        {
           if( !_context.Users.Any(p => p.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
                            }
            return result; 
        }

        private string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();
            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }


            return builder.ToString();
        }

        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == username));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }
    }
}
