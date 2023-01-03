namespace WebshopAPI.data
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public ESeverity Severity { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string UserID { get; set; }
    }
}
