using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid Id, string FirstName, string LastName, string UserName, string Password, string Email) : IRequest<Result<string>>;
