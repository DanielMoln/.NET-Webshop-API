namespace WebshopAPI.lib.Exceptions
{
    public class BodyEmptyException : Exception
    {
        public int statusCode = ExceptionStatusCodes.BODY_EMPTY;

        public BodyEmptyException()
        {

        }
    }
}
