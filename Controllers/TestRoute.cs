using LibraryAPI.Lib.Services;

namespace WebshopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestRoute : ControllerBase
    {
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                using (SQL sql = new SQL())
                {
                    Encryption encryption = Encryption.Initialize();
                    User user = new User()
                    {
                        UserID = "test@gmail.com",
                        UserType = EUserType.Admin,
                        Description = "",
                        Enabled = true,
                        Password = encryption.EncyptPassword("asd123"),
                        LastLogon = DateTime.Now,
                        Name = "János"
                    };
                    sql.Users.Add(user);
                    await sql.SaveChangesAsync();

                    return Ok(new APIResponse()
                    {
                        StatusCode = 200,
                        Data = "",
                        Date = DateTime.Now,
                        Message = "Felhasználó rögzítve!"
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
