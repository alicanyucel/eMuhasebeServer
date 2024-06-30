

using AutoMapper;
using eMuhasebeServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.UpdateUser;

internal sealed record UpdateUserCommand(Guid Id, string FirstName, string LastName, string UserName, string Password, string Email):IRequest<Result<string>>;
internal sealed class UpdateUserCommandHandler(IMapper mapper, UserManager<AppUser> userManager) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(request.Id.ToString());
        if (appUser == null)
        {
            return Result<string>.Failure("kullanıcı bulunamadi");
        }
        if(appUser.UserName!=request.UserName)
        {
            bool IsUserNameExits=await userManager.Users.AnyAsync(p=>p.UserName==request.UserName,cancellationToken);
            if (IsUserNameExits)
            {
                return Result<string>.Failure("bu kullanıcı adı daha once alınmıs.");
            }
        }
    }
}