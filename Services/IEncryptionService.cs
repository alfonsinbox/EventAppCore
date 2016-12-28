namespace EventAppCore.Services
{
    public interface IEncryptionService
    {
        string CreatePasswordSaltHash(string password);
        bool IsPasswordValid(string password, string saltHash);
    }
}