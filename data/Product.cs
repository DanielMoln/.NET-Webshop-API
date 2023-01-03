namespace WebshopAPI.data
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool Available { get; set; }
    }
}
