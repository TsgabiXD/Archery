using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Archery.Api.Auth;
using Archery.Model;
using Archery.Repository;

namespace Archery.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : ArcheryController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ArcheryContext _context;
    private readonly TokenService _tokenService;
    private readonly UserRepository _repository;

    public AuthController(ILogger<EventController> logger,
                          UserManager<IdentityUser> userManager,
                          ArcheryContext context,
                          TokenService tokenService,
                          UserRepository repository) : base(logger)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
        _repository = repository;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(AuthRequest request)
    {
        try
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

                var userInDb = _context.IdentityUser.FirstOrDefault(u => u.UserName == request.Username);
                var simpleUser = _context.User.FirstOrDefault(u => u.NickName == request.Username);

                if (userInDb is null)
                    throw new Exception("There has been some IdentityUser adding problem!");

                if (simpleUser is null)
                {
                    if (request.FirstName != null && request.LastName != null)
                        _repository.AddUser(new()
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            NickName = request.Username
                        });

                    simpleUser = _context.User.SingleOrDefault(u => u.NickName == request.Username);

                    if (simpleUser is null)
                        throw new Exception("Problems at adding the user!");


                    _context.SaveChanges();
                }

                var accessToken = _tokenService.CreateToken(userInDb, simpleUser);
                return Ok(new AuthResponse { Token = accessToken });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);

            return BadRequest(ModelState);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
        var currentUser = _context.User.FirstOrDefault(u => u.NickName == request.Username);

        if (userInDb is null || currentUser is null)
            return Unauthorized();

        var accessToken = _tokenService.CreateToken(userInDb, currentUser);

        await _context.SaveChangesAsync();

        return Ok(new AuthResponse
        {
            Token = accessToken
        });
    }
}