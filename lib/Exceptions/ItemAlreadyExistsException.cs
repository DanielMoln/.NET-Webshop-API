namespace WebshopAPI.lib.Exceptions
{
    public class ItemAlreadyExistsException : Exception
    {
        public int statusCode = ExceptionStatusCodes.ITEM_ALREADY_EXISTS;

        public ItemAlreadyExistsException()
        {

        }
    }
}
