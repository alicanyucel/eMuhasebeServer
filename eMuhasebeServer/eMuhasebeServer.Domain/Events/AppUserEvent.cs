
using eMuhasebeServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eMuhasebeServer.Domain.Events;
public sealed class  AppUserEvent:INotification
{
    public Guid UserId { get; set; }    
    public AppUserEvent(Guid userId)
    {
        UserId = userId;
    }
}
public sealed class SendConfirmEmail(UserManager<AppUser> userManager  ) : INotificationHandler<AppUserEvent>
{
    public async Task Handle(AppUserEvent notification, CancellationToken cancellationToken)
    {
        AppUser? appUser=await userManager.FindByIdAsync(notification.UserId.ToString());
        if(appUser == null)
        {
            // onay maili gonder
        }
    }
}
