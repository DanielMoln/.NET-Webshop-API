namespace WebshopAPI.lib.Exceptions
{
    public class EmailNotValidException : Exception
    {
        public int statusCode = ExceptionStatusCodes.EMAIL_NOT_VALID;

        public EmailNotValidException()
        {  }
    }
}
