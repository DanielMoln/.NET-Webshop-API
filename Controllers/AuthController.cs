using DuncansbyAPI.lib;
using DuncansbyAPI.lib.Services;
using LibraryAPI.Lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebshopAPI.data;
using WebshopAPI.lib;
using WebshopAPI.lib.Services;

namespace WebshopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManagerService userManagerService;

        public AuthController(UserManagerService userManagerService)
        {
            this.userManagerService = userManagerService;
        }

        [HttpPost("logon")]
        public async Task<IActionResult> Logon([FromBody] Logon logon)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (logon == null)
                {
                    throw new BodyEmptyException();
                }

                if (logon.Email.IsNullOrEmpty())
                {
                    throw new MandatoryPropertyEmptyException("email");
                }
                if (logon.Password.IsNullOrEmpty())
                {
                    throw new MandatoryPropertyEmptyException("password");
                }

                using (SQL sql = new SQL())
                {
                    if (!sql.Users.Any(a => a.UserID == logon.Email))
                    {
                        throw new UserNotFoundException();
                    }

                    User user = sql.Users.Single(a => a.UserID == logon.Email);

                    if (user.Enabled == false)
                    {
                        await $"Nem aktiválva ez a fiók!".WriteInformationLogAsync(logon.Email);
                        throw new UserNotActivatedException();
                    }

                    string password = user.Password;

                    if (password == null)
                    {
                        await $"Nem létezik jelszó ehhez a fiókhoz!".WriteErrorLogAsync(logon.Email);
                        throw new PasswordNotFoundException();
                    }

                    Encryption enc = Encryption.Initialize(password);

                    if (enc.Validate(logon.Password) == false)
                    {
                        await $"Sikertelen belépés!".WriteErrorLogAsync(logon.Email);
                        throw new PasswordNotMatchException();
                    }

                    AccessToken accessToken = TokenHandlerService.GenerateToken(user);

                    await $"Sikeres belépés!".WriteInformationLogAsync(logon.Email);
                    user.LastLogon = DateTime.Now;
                    sql.SaveChangesAsync();

                    return Ok(new APIResponse()
                    {
                        Data = accessToken
                    });
                }
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés body tartalma üres!";
            }
            catch (PasswordNotMatchException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A megadott jelszó téves!";
            }
            catch (PasswordNotFoundException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Nem tartozik jelszó ehhez a felhasználóhoz!";
            }
            catch (UserNotActivatedException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A felhasználó nem lett aktiválva!";
            }
            catch (UserNotFoundException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez a felhasználó nem található!";
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = e.Message;
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = $"{e.GetExceptionMessage()}";
            }

            return Unauthorized(response);
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> NewUser([FromBody] Registration regData)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (regData == null)
                {
                    throw new BodyEmptyException();
                }

                ObjectValidatorService<Registration> validator = new ObjectValidatorService<Registration>(regData);
                validator.IsValid();
                await userManagerService.CreateUser(regData);
                await $"Új felhasználó regisztrálva lett ({regData.UserID})".WriteInformationLogAsync("System");

                response.StatusCode = 200;
                response.Data = regData;

                return Ok(response);
            }
            catch (EmailNotValidException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Az email cím helytelen vagy nem létezik!";
            }
            catch (UserAlreadyExistsException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez a felhasználó már létezik!";
            }
            catch (UserNotFoundException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez a felhasználó nem található!";
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Minden mezőt kötelezően ki kell tölteni!";
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma üres!";
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }
    }
}
