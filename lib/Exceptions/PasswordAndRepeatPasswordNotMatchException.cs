namespace WebshopAPI.lib.Exceptions
{
    public class PasswordAndRepeatPasswordNotMatchException : Exception
    {
        public int statusCode = ExceptionStatusCodes.PASSWORD_AND_REPEATED_PASSWORD_NOT_MATCH;
        public PasswordAndRepeatPasswordNotMatchException()
        {

        }
    }
}
