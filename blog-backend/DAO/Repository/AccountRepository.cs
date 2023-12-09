using blog_backend.DAO.Database;
using blog_backend.DAO.Model;
using blog_backend.Entity;
using blog_backend.Service;
using blog_backend.Service.Mappers;
using blog_backend.Service.Repository;


namespace blog_backend.DAO.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly BlogDbContext _dbContext;
    private readonly GenerateTokenService _tokenService;

    public AccountRepository(BlogDbContext dbContext, GenerateTokenService tokenService)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
    }

    public async Task<TokenDTO> Register(AuthorizationDTO request, string hashPassword)
    {
        var user = AuthDtoMapper.Map(request, hashPassword);
        await _dbContext.User.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        var token = _tokenService.GenerateToken(user);
        return await Task.FromResult(new TokenDTO { Token = token });
    }

    public Task GetUserName(string userId)
    {
        return Task.FromResult(_dbContext.User.FirstOrDefault(u => u.Id.ToString() == userId)?.FullName);
    }

    public async Task<List<User>> GetAuthors()
    {
        return await Task.FromResult(_dbContext.User.ToList());
    }

    public Task EditUser(User user, string userId)
    {
        
        _dbContext.User.Update(user);
        _dbContext.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<User?> GetUserById(string id)
    {
        return Task.FromResult(_dbContext.User.FirstOrDefault(u => u.Id.ToString() == id));
    }

    public void LogoutUser(string token)
    {
        _tokenService.SaveExpiredToken(token);
    }

    public async Task<User?> GetUserByEmail(string userEmail)
    {
        return await Task.FromResult(_dbContext.User.FirstOrDefault(u => u.Email == userEmail));
    }
}