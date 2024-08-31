namespace CribQuest.Backend.Services;

public class AuthService : IAuth
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtAuth _jwtAuth;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpContext;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IJwtAuth jwtAuth)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _jwtAuth = jwtAuth;
        _httpContext = httpContextAccessor.HttpContext;
    }

    /// <inheritdoc />
    public async Task<GeneralResponse> RegisterUser(SignUpPayload payload)
    {
        #region Checks
        
        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user is not null)
            return new GeneralResponse()
                { Message = $"User with the email {payload.Email} exists" };

        #endregion

        user = _mapper.Map<User>(payload);
        user.PasswordHash = payload.Password;

        var createUserResult = await _userManager.CreateAsync(user, payload.Password);
        if (!createUserResult.Succeeded)
            return new GeneralResponse()
            {
                Message = $"Create user failed {createUserResult?.Errors?.First()?.Description}",
            };

        // send mail

        return new GeneralResponse() { Message = "User created successfully", Success = true };
    }

    /// <inheritdoc />
    public async Task<GeneralResponse> LoginUser(SignInPayload payload)
    {
        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user is null) return new GeneralResponse() { Message = "Invalid Email or password" };

        //Verifies user password
        var result = await _signInManager.CheckPasswordSignInAsync(user, payload.Password, false);
        if (!result.Succeeded)
            return new GeneralResponse() { Message = "Invalid Email or password" };


        var token = _jwtAuth.Authentication(user);
        _httpContext.Response.Cookies.Append("token", token, new CookieOptions()
        {
            Expires = DateTime.Now.AddDays(10),
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        });

        return new GeneralResponse()
        {
            Message = "Login Successful",
            Data = user.Id,
            Success = true,
        };
    }
}