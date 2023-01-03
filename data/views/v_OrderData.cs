using Microsoft.EntityFrameworkCore.Metadata;

namespace WebshopAPI.data.views
{
    [Keyless]
    [Table("v_OrderDatas")]
    public class v_OrderData
    {
        [Required]
        public int OrderID { get; set; }
        [Required]
        [Column("UserEmail")]
        public string UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
    }
}
