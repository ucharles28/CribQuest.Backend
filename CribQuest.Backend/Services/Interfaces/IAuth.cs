namespace CribQuest.Backend.Services.Interfaces;

public interface IAuth
{
    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="payload">The new user details</param>
    /// <returns></returns>
    Task<GeneralResponse> RegisterUser(SignUpPayload payload);
    /// <summary>
    /// Logs in a user
    /// </summary>
    /// <param name="payload">Login payload</param>
    /// <returns></returns>
    Task<GeneralResponse> LoginUser(SignInPayload payload);
}