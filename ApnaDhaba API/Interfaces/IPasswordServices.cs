namespace ApnaDhaba_API.Interfaces
{
    public interface IPasswordServices
    {
        string HashPassword(string password);

        bool VerifyPassword(string InputPassword, string HashedPasswordDB);
    }
}