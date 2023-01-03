using DuncansbyAPI.lib.Services;
using DuncansbyAPI.lib.Utils;
using DuncansbyAPI.lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebshopAPI.lib.Services;
using WebshopAPI.data;
using WebshopAPI.data.views;
using WebshopAPI.lib;

namespace WebshopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : AdminBaseController
    {
        public UserManagerService userManagerService;

        public AdminController(UserManagerService userManagerService)
        {
            this.userManagerService = userManagerService;
        }

        [HttpGet("Users")]
        public IActionResult ListUsers()
        {   
            APIResponse response = new APIResponse();

            try
            {
                IEnumerable<v_User> users = userManagerService.ListUsers();

                response.Date = DateTime.Now;
                response.Data = users;
                response.Message = "";
                response.StatusCode = 200;

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }

            return BadRequest(response);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string UserID)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (UserID.IsNullOrEmpty())
                {
                    throw new MandatoryPropertyEmptyException("userid");
                }

                User findedUser = userManagerService.GetUser(UserID);
                await $"{UserID} felhasználó adatai sikeresen le lettek kérve!".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = findedUser;

                return Ok(response);
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
            }
            catch (UserNotFoundException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez a felhasználó nem található!";
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (user == null)
                {
                    throw new BodyEmptyException();
                }

                if (user.UserID.IsNullOrEmpty())
                {
                    throw new MandatoryPropertyEmptyException("user id");
                }

                await userManagerService.UpdateUser(user);
                await $"{user.UserID} sikeresen módosítva lett!".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = user;

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

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChange pwdChange)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (pwdChange == null)
                {
                    throw new BodyEmptyException();
                }

                ObjectValidatorService<PasswordChange> validator = 
                    new ObjectValidatorService<PasswordChange>(pwdChange);
                validator.IsValid();
                await userManagerService.ChangePassword(pwdChange);
                await $"{pwdChange.UserID} jelszava sikeresen módosítva lett!".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = "";

                return Ok(response);
            }
            catch (BadOldPasswordException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Rossz jelszó!";
            }
            catch (PasswordAndRepeatPasswordNotMatchException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A két új jelszó nem eggyezik!";
            }
            catch (UserPasswordAndNewPasswordSameException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A felhasználó jelenlegi jelszava megegyezik az új jelszóval!";
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

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteUser([FromBody] User user)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (user.UserID.IsNullOrEmpty())
                {
                    throw new MandatoryPropertyEmptyException("userid");
                }

                await userManagerService.DeleteUser(user.UserID);
                await $"{user.UserID} felhasználó sikeresen törölve lett!".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = "";

                return Ok(response);
            } 
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
            }
            catch (UserNotFoundException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez a felhasználó nem található!";
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
