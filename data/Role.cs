namespace WebshopAPI.data
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [Required]
        public int RoleID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
