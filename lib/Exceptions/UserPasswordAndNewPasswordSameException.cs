namespace WebshopAPI.lib.Exceptions
{
    public class UserPasswordAndNewPasswordSameException : Exception
    {
        public int statusCode = ExceptionStatusCodes.USER_PASSWORD_AND_NEW_PASSWORD_NOT_SAME;

        public UserPasswordAndNewPasswordSameException()
        {

        }
    }
}
