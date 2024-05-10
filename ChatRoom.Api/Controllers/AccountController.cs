using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Contracts.Wrappers;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Contracts.Dtos.Account;

namespace ChatRoom.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
	[HttpPost(nameof(Authenticate))]
	public async Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
		   => await accountService.Authenticate(request);


	[HttpPost(nameof(Register))]
	public async Task<BaseResult> Register(RegistrationRequest request)
		   => await accountService.RegisterUser(request);

}
