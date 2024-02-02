using Common.Email;
using Microsoft.AspNetCore.Mvc;
using User.Core.Requests;
using User.Dal.Interfaces;

namespace Goodini.Controllers;

[ApiController]
[Route("api/users")]
public class UserController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _repository;

    public UserController(ILogger<UserController> logger, IUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IResult> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _repository.GetUsersAsync(cancellationToken);
        return users.Count == 0 ? Results.NotFound() : Results.Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetUserAsync(id, cancellationToken);
            return user == null ? Results.NotFound() : Results.Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Results.BadRequest();
        }
        
    }

    [HttpPost]
    public async Task<IResult> CreateUserAsync([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email) && EmailExtension.IsEmailValid(request.Email))
            return Results.BadRequest();

        try
        {
            var user = await _repository.CreateUserAsync(request, cancellationToken);
            return Results.Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Results.BadRequest();
        }
       
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateUserAsync([FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken, int id)
    {
        try
        {
            if (!string.IsNullOrEmpty(request.Email) && !EmailExtension.IsEmailValid(request.Email))
                return Results.BadRequest();
            
            await _repository.UpdateUserAsync(id, request, cancellationToken);
            return Results.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Results.BadRequest();
        }
        
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var isRemoved = await _repository.RemoveUserAsync(id, cancellationToken);
            return isRemoved ? Results.Ok() : Results.NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Results.BadRequest();
        }
       
    }
}