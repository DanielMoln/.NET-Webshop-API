namespace WebshopAPI.lib.Exceptions
{
    public class MandatoryPropertyEmptyException : Exception
    {
        public int statusCode = ExceptionStatusCodes.MANDATORY_DATA_EMPTY;

        public MandatoryPropertyEmptyException(string field) : base($"A(z) {field} mező nem lehet üres!")
        {

        }
    }
}
