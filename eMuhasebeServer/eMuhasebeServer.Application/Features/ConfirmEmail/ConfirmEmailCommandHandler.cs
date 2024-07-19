

using eMuhasebeServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ConfirmEmail;

internal sealed class ConfirmEmailCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<ConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
       AppUser? appUser=await userManager.FindByEmailAsync(request.Email);
        if (appUser is null)
        {
            return "Mail adresi sistemde kayıtlı değil";
        }
        if(appUser.EmailConfirmed)
        {
            return "mail adresi zaten sistemde onayli";
        }
        appUser.EmailConfirmed = true;
        await userManager.UpdateAsync(appUser);
        return "Mail adresiniz onaylandi";
    }
}