namespace WebshopAPI.data
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Required]
        public int OrderID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int ProductID { get; set; } 

        [Required]
        [Column("Date")]
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        [Required]
        public int Amount { get; set; }
        [Required]
        public bool Deleted { get; set; }
    }
}
