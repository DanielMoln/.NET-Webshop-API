namespace WebshopAPI.data
{
    [Table("UserRoles")]
    public class UserRole
    {
        [Key]
        [EmailAddress]
        [Required]
        public string UserID { get; set; }
        [Key]
        [Required]
        public int RoleID { get; set; }
    }
}
