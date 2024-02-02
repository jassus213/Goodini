using User.Core.Requests;

namespace User.Dal.Interfaces;

public interface IUserRepository : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Получения всех пользователей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public Task<IList<Core.User>> GetUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Поиск информации о конкретном пользователе 
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public Task<Core.User?> GetUserAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    /// <param name="request">Тело запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <param name="isSaveRequired">По стандарту = true, если необходимо создать пользователя и сразу сохранит его в базу</param>
    public Task<Core.User> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken,
        bool isSaveRequired = true);

    /// <summary>
    /// Обновление информации у конкретного пользователя
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <param name="isSaveRequired">По стандарту = true, если необходимо создать пользователя и сразу сохранит его в базу</param>
    /// <returns></returns>
    public Task UpdateUserAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken,
        bool isSaveRequired = true);

    /// <summary>
    /// Удаления конкретного пользователя
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public Task<bool> RemoveUserAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Сохранения внесенных измнений 
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}