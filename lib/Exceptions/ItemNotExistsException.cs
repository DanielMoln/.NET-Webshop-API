namespace WebshopAPI.lib.Exceptions
{
    public class ItemNotExistsException : Exception
    {
        public int statusCode = ExceptionStatusCodes.ITEM_NOT_FOUND;

        public ItemNotExistsException()
        {

        }
    }
}
