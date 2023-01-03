using LibraryAPI.Lib.Services;
using WebshopAPI.data;
using WebshopAPI.data.views;
using WebshopAPI.lib;
using WebshopAPI.lib.Exceptions;
using WebshopAPI.lib.Services;

namespace DuncansbyAPI.lib.Services
{
    public class UserManagerService
    {
        public IEnumerable<v_User> ListUsers()
        {
            using (SQL sql = new SQL())
            {
                IEnumerable<v_User> users = sql.v_Users.ToList();
                return users;
            }
        }

        public async Task CreateUser(Registration regData)
        {
            using (SQL sql = new SQL())
            {
                if (sql.Users.Any(a => a.UserID == regData.UserID))
                {
                    throw new UserAlreadyExistsException();
                }

                Encryption encryption = Encryption.Initialize();

                User user = new User()
                {
                    UserID = regData.UserID,
                    UserType = EUserType.User,
                    Description = "",
                    Enabled = false,
                    LastLogon = DateTime.Now,
                    Name = regData.UserName,
                    Password = encryption.EncyptPassword(regData.Password)
                };

                sql.Users.Add(user);
                await sql.SaveChangesAsync();
            }

            using (SQL sql = new SQL())
            {
                sql.UserRoles.Add(new UserRole()
                {
                    UserID = regData.UserID,
                    RoleID = 1
                });
                await sql.SaveChangesAsync();
            }
        }

        public async Task UpdateUser(User user)
        {
            using (SQL sql = new SQL())
            {
                if (!sql.Users.Any(a => a.UserID == user.UserID))
                {
                    throw new UserNotFoundException();
                }

                User findedUser = sql.Users.Single(a => a.UserID == user.UserID);

                if (findedUser.Name != null) findedUser.Name = user.Name;
                if (findedUser.Description != null) findedUser.Description = user.Description;
                if (findedUser.UserType != null) findedUser.UserType = user.UserType;
                if (findedUser.Enabled != null) findedUser.Enabled = user.Enabled;

                await sql.SaveChangesAsync();
                await "Felhasználó adatok módosítva lettek!".WriteInformationLogAsync(user.UserID);
            }
        }

        public async Task ChangePassword(PasswordChange passwordChange)
        {
            using (SQL sql = new SQL())
            {
                // user exists check
                if (!sql.Users.Any(a => a.UserID == passwordChange.UserID))
                {
                    throw new UserNotFoundException();
                }

                // required attributes check
                ObjectValidatorService<PasswordChange> usr = new ObjectValidatorService<PasswordChange>(passwordChange);
                usr.IsValid();

                // the to new password same or not
                if (passwordChange.NewPassword != passwordChange.NewPasswordRepeat)
                {
                    throw new PasswordAndRepeatPasswordNotMatchException();
                }

                // getting user from the db
                User user = sql.Users.Single(a => a.UserID == passwordChange.UserID);

                // removing salts from the password
                Encryption e = Encryption.Initialize(user.Password);

                // if the parameter's password and the user's password same
                if (e.Validate(passwordChange.Password))
                {
                    // if the user's password isn't same as the newpassword
                    if (!e.Validate(passwordChange.NewPasswordRepeat))
                    {
                        e = Encryption.Initialize();
                        user.Password = e.EncyptPassword(passwordChange.NewPassword);
                        await sql.SaveChangesAsync();
                    }
                    // if the new password isn't same as the new repeated password
                    else
                    {
                        throw new UserPasswordAndNewPasswordSameException();
                    }
                }
                // if the parameter's old password is not equal with the user's password
                else
                {
                    throw new BadOldPasswordException();
                }
            }
        }

        public async Task DeleteUser(string UserID)
        {
            using (SQL sql = new SQL())
            {
                // user exists check
                if (!sql.Users.Any(a => a.UserID == UserID))
                {
                    throw new UserNotFoundException();
                }

                User findedUser = sql.Users.Single(a => a.UserID == UserID);

                findedUser.Enabled = false;
                await sql.SaveChangesAsync();
            }
        }

        public User GetUser(string UserID)
        {
            using (SQL sql = new SQL())
            {
                // user exists check
                if (!sql.Users.Any(a => a.UserID == UserID))
                {
                    throw new UserNotFoundException();
                }

                User findedUser = sql.Users.Single(a => a.UserID == UserID);

                return findedUser;
            }
        }
    }
}
