namespace WebshopAPI.data
{
    public class Logon
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
