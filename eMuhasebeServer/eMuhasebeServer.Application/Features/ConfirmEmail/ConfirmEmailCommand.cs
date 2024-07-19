using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ConfirmEmail;

public sealed record ConfirmEmailCommand(string Email):IRequest<Result<string>>;
