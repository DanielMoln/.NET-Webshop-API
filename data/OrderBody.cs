namespace WebshopAPI.data
{
    public class OrderBody
    {
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
    }
}
