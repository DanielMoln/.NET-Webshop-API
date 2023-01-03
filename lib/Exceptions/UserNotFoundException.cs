namespace WebshopAPI.lib.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public int statusCode = ExceptionStatusCodes.USER_NOT_FOUND;
        public UserNotFoundException()
        {

        }
    }
}
