namespace ActivitySeeker.Bll.Interfaces;

public interface IPasswordHasher
{
    string Generate(string password);

    bool Verify(string password, string hashedPassword);
}