using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.DeleteuserById;
public sealed record DeleteUserByIdCommand(Guid Id) : IRequest<Result<string>>;
