public class RoleManagerService
{
    public bool isExistsRole(int roleID)
    {
        using (SQL sql = new SQL())
        {
            if (sql.Roles.Any(a => a.RoleID == roleID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
