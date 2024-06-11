using ApnaDhaba_API.Interfaces;
using System.Security.Cryptography;

namespace ApnaDhaba_API.Services
{
    public class PasswordServices : IPasswordServices
    {
        string IPasswordServices.HashPassword(string password)
        {
            var hmac = new HMACSHA512();

            hmac.Key = RandomNumberGenerator.GetBytes(64); // for adding randomness in algo

            byte[] passwordSalt = hmac.Key;
            byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // computing HASH

            return string.Join('.', Convert.ToBase64String(passwordSalt), Convert.ToBase64String(passwordHash)); //returning and storing Data in DB in base64 format as they are just random string and very hard to decode
        }

        bool IPasswordServices.VerifyPassword(string InputPassword, string HashedPasswordDB)
        {
            var element = HashedPasswordDB.Split('.');
            var passwordSalt = Convert.FromBase64String(element[0]);
            var passwordHash = Convert.FromBase64String(element[1]);

            var hmac = new HMACSHA512() { Key = passwordSalt };
            byte[] inputPassHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(InputPassword));

            return passwordHash.SequenceEqual(inputPassHash);

            //during login the password entered is recomputed or hash is created and matched with the one stored in DB if matched this indicates that the user credentials are valid and we can givve access to the user...
        }
    }
}