using Microsoft.EntityFrameworkCore;
using User.Core.Requests;
using User.Dal.Interfaces;

namespace User.Dal.PostgreSql;

public class UserRepository : IUserRepository
{
    private readonly UserContext _userContext;
    private bool _isDisposed = false;

    public UserRepository(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<IList<Core.User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var dto = await _userContext.Users
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return dto.Select(x => x.Map()).ToList();
    }

    public async Task<Core.User?> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        var dto = await _userContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);

        return dto?.Map();
    }
    
    public async Task<Core.User> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken,
        bool isSaveRequired = true)
    {
        var dto = new UserEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        await _userContext.Users.AddAsync(dto, cancellationToken);
        if (isSaveRequired)
            await _userContext.SaveChangesAsync(cancellationToken);

        return dto.Map();
    }
    
    public async Task UpdateUserAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken,
        bool isSaveRequired = true)
    {
        var dto = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (dto == null)
            return;

        if (!string.IsNullOrEmpty(request.Email))
            dto.Email = request.Email;

        if (!string.IsNullOrEmpty(request.FirstName))
            dto.FirstName = request.FirstName;

        if (!string.IsNullOrEmpty(request.LastName))
            dto.LastName = request.LastName;

        if (isSaveRequired)
            await _userContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> RemoveUserAsync(int id, CancellationToken cancellationToken)
    {
        var queryTable = _userContext.Users.EntityType.GetSchemaQualifiedTableName();
        var query = $"DELETE FROM \"{queryTable}\" WHERE \"Id\" = {id}";
        var affectedRows = await _userContext.Database.ExecuteSqlRawAsync(query, cancellationToken);
        return affectedRows == 0 ? false : true;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        _userContext.SaveChangesAsync(cancellationToken);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            _userContext.Dispose();
            _isDisposed = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            await _userContext.DisposeAsync();
            _isDisposed = true;
        }
    }
}