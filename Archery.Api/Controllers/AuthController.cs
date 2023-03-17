using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Archery.Api.Helper;
using Archery.Model;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : ArcheryController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ArcheryContext _context;
    private readonly TokenService _tokenService;

    public AuthController(ILogger<EventController> logger,
                          UserManager<IdentityUser> userManager,
                          ArcheryContext context,
                          TokenService tokenService) : base(logger)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(AuthRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userManager.CreateAsync(
            new IdentityUser { UserName = request.Username },
            request.Password
        );

        if (result.Succeeded)
        {
            request.Password = "";
            return CreatedAtAction(nameof(Register), request);
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)        
            return BadRequest(ModelState);        

        var managedUser = await _userManager.FindByNameAsync(request.Username);

        if (managedUser == null)        
            return BadRequest("Bad credentials");
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

        if (!isPasswordValid)        
            return BadRequest("Bad credentials");

        var userInDb = _context.IdentityUser.FirstOrDefault(u => u.UserName == request.Username);

        if (userInDb is null)
            return Unauthorized();

        var accessToken = _tokenService.CreateToken(userInDb);

        await _context.SaveChangesAsync();

        return Ok(new AuthResponse
        {
            Username = userInDb.UserName,
            Token = accessToken,
        });
    }
}