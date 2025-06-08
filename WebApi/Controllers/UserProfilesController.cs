using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "UserOnly")]
public class UserProfilesController(IUserProfileService userProfileService) : ControllerBase
{
    private readonly IUserProfileService _userProfileService = userProfileService;


    // READ
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserProfile>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userProfileService.GetAllUserProfilesAsync();
        var userProfiles = result.Data;

        return result.Succeeded ? Ok(userProfiles) : StatusCode(result.StatusCode, result.ErrorMessage);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfile))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserProfileById(string id)
    {
        var result = await _userProfileService.GetUserProfileByIdAsync(id);
        var userProfile = result.Data;

        return result.Succeeded ? Ok(userProfile) : StatusCode(result.StatusCode, result.ErrorMessage);
    }
}
