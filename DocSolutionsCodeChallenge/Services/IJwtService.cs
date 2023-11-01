namespace DocSolutionsCodeChallenge.Services
{
    public interface IJwtService
    {
        string GenerateToken(string user);
    }
}