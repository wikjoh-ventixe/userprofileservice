using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfilesController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
}
