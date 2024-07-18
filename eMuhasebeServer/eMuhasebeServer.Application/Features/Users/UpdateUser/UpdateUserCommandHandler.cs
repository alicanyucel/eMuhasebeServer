

using AutoMapper;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(IMapper mapper,IMediator mediator, UserManager<AppUser> userManager) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(request.Id.ToString());
        bool isMailChange = false;
        if (appUser == null)
        {
            return Result<string>.Failure("kullanıcı bulunamadi");
        }
        if (appUser.UserName != request.UserName)
        {
            bool IsUserNameExits = await userManager.Users.AnyAsync(p => p.UserName == request.UserName, cancellationToken);
            if (IsUserNameExits)
            {
                return Result<string>.Failure("bu kullanıcı adı daha once alınmıs.");
            }
        }
        if (appUser.Email != request.Email)
        {
            bool isEmailExists = await userManager.Users.AnyAsync(p => p.Email == request.Email, cancellationToken);
            if (isEmailExists)
            {
                return Result<string>.Failure("bu emaila dresi daha önce alınmış");
            }
            isMailChange = true;
            appUser.EmailConfirmed = false;

        }
        mapper.Map(request, appUser);
        IdentityResult identityResult = await userManager.UpdateAsync(appUser);
        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());

        }
        string token = await userManager.GeneratePasswordResetTokenAsync(appUser);
        identityResult = await userManager.ResetPasswordAsync(appUser, token, request.Password);
        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
        }
        if(isMailChange)
        {
            await mediator.Publish(new AppUserEvent(appUser.Id));
        }
        return "kullanıcı güncellendi";
    }
}