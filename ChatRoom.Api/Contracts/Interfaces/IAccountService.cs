using ChatRoom.Api.Contracts.Dtos;
using ChatRoom.Api.Contracts.Wrappers;
using ChatRoom.Api.Domain.Models;

namespace ChatRoom.Api.Contracts.Interfaces;
public interface IAccountService
{
    Task<BaseResult> RegisterUser(RegistrationRequest request);
    Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
}