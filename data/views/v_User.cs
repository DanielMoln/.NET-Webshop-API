namespace WebshopAPI.data.views
{
    [Keyless]
    [Table("v_Users")]
    public class v_User
    {
        public string Role { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime LastLogon { get; set; }
        public bool Enabled { get; set; }
    }
}
