
using eMuhasebeServer.Domain.Entities;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eMuhasebeServer.Domain.Events;

public sealed class SendConfirmEmailEvent(UserManager<AppUser> userManager, IFluentEmail fluentEmail) : INotificationHandler<AppUserEvent>
{
    public async Task Handle(AppUserEvent notification, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(notification.UserId.ToString());
        if (appUser is not null)
        {
            await fluentEmail.To(appUser.Email).Subject("mail onayı").Body(CreateBody(appUser),true).SendAsync(cancellationToken);
        }
    }
    private string CreateBody(AppUser appUser)
    {
        string body = $@" mail  adresinizi onaylamak için aşağıdaki linki tıklayınız.<a href='http://localhost:4200/confirm-email/{appUser.Email}' target='_blank'>maili onaylamak için tıklayınız</a>";
        return body;
    }
}
