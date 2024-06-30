using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.CreateUser;
internal sealed record CreateUserCommand (string FirstName,string LastName,string UserName,string Password,string Email):IRequest<Result<string>>;
