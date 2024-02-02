namespace User.Dal;

public static class UserMapper
{
    public static Core.User Map(this UserEntity entity)
    {
        return new Core.User
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email
        };
    }
}