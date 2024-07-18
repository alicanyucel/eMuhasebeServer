
using AutoMapper;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.CreateUser;

internal sealed class CreateUserCommandHandler(IMapper mapper,IMediator mediator,UserManager<AppUser> userManager) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool isUserNameExits = await userManager.Users.AnyAsync(p => p.UserName == request.UserName, cancellationToken);
        if (isUserNameExits)
        {
            return Result<string>.Failure("bu kulllanıcı adı daha önce kullanılmış");
        }
        bool isEmailExits= await userManager.Users.AnyAsync(p=>p.Email == request.Email,cancellationToken);
        if (isEmailExits)
        {
            return Result<string>.Failure("bu mail adresi daha önce alınmış");
        }
        AppUser appUser = mapper.Map<AppUser>(request);
        IdentityResult ıdentityResult = await userManager.CreateAsync(appUser,request.Password);
        if (ıdentityResult.Succeeded)
        {
            return Result<string>.Failure(ıdentityResult.Errors.Select(s => s.Description).ToList());
        }
        await mediator.Publish(new AppUserEvent(appUser.Id));
        return "kullanıcı kaydı başarıyla tamamlandı";
    }
}
