namespace WebshopAPI.data
{
    [Table("Users")]
    public class User
    {
        [Key]
        [EmailAddress]
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public EUserType UserType { get; set; }
        [Required]
        public DateTime LastLogon { get; set; } = DateTime.Now;
        [Required]
        public bool Enabled { get; set; }
        public string? Description { get; set; }
    }
}
