namespace WebshopAPI.data
{
    public class PasswordChange
    {
        [Required]
        [EmailAddress]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPasswordRepeat { get; set; }
    }
}
