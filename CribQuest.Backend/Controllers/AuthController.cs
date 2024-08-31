using System.Net;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;


namespace CribQuest.Backend.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        /// <summary>
        ///  Registers a new user 
        /// </summary>
        /// <remarks>
        /// Password Requirements:
        ///  - Password cannot be null
        ///  - Password must be at least 8 characters
        ///  - Password must have at least one non alphanumeric character
        ///  - Password must have at least one lowercase('a' - 'z') 
        ///  - Password must have at least one uppercase('A' - 'Z')
        /// AccountType & Integer representation (To be Reviewed):
        /// - Admin: 0
        /// - Agent: 1
        /// - Customer: 2
        /// </remarks>
        /// <param name="payload">The details of the new user</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SignUp")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GeneralResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GeneralResponse))]
        public async Task<IActionResult> SignUp(SignUpPayload payload)
        {
            var body = await payload.GetJsonBody<SignUpPayload, SignUpPayloadValidation>();

            //Check if all the fields are correctly filled
            if (!body.IsValid) return body.ToBadRequest();

            var response = await _auth.RegisterUser(payload);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        
        /// <summary>
        /// Signs in a registered user
        /// creates a new session
        /// </summary>
        /// <param name="payload">Login details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SigIn")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GeneralResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GeneralResponse))]
        public async Task<IActionResult> SignIn(SignInPayload payload)
        {
            var body = await payload.GetJsonBody<SignInPayload, SignInPayloadValidation>();

            //Check if all the fields are correctly filled
            if (!body.IsValid) return body.ToBadRequest();

            var response = await _auth.LoginUser(payload);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
