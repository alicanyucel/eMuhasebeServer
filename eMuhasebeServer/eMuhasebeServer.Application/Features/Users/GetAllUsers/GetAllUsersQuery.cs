

using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.GetAllUsers;
internal sealed record GetAllUsersQuery() : IRequest<Result<List<AppUser>>>;
