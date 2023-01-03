namespace WebshopAPI.lib
{
    public static class Extensions
    {
        public static string GetExceptionMessage(this Exception e)
        {
            return (e.InnerException != null ? (e.InnerException.InnerException != null ? (e.InnerException.InnerException.InnerException != null ? e.InnerException.InnerException.InnerException.Message : e.InnerException.InnerException.Message) : e.InnerException.Message) : e.Message);
        }

        public static async Task WriteCriticalLogAsync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Critical,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                await sql.Logs.AddAsync(log);
                await sql.SaveChangesAsync();
            }
        }
        public static void WriteCriticalLogSync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Critical,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                sql.Logs.Add(log);
                sql.SaveChanges();
            }
        }

        public static async Task WriteErrorLogAsync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Error,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                await sql.Logs.AddAsync(log);
                await sql.SaveChangesAsync();
            }
        }
        public static void WriteErrorLogSync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Error,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                sql.Logs.Add(log);
                sql.SaveChanges();
            }
        }

        public static async Task WriteWarningLogAsync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Warning,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                await sql.Logs.AddAsync(log);
                await sql.SaveChangesAsync();
            }
        }
        public static void WriteWarningLogSync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Warning,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                sql.Logs.Add(log);
                sql.SaveChanges();
            }
        }

        public static async Task WriteInformationLogAsync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Information,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                await sql.Logs.AddAsync(log);
                await sql.SaveChangesAsync();
            }
        }
        public static void WriteInformationLogSync(this string message, string UserID)
        {
            using (SQL sql = new SQL())
            {
                Log log = new Log()
                {
                    UserID = UserID,
                    Severity = ESeverity.Information,
                    Message = "[" + UserID + "] " + message,
                    Date = DateTime.Now
                };

                sql.Logs.Add(log);
                sql.SaveChanges();
            }
        }
        public static int CountOf(this string message, char character)
        {
            int count = 0;
            foreach (char c in message)
            {
                if (c == character)
                {
                    count++;
                }
            }
            return count;
        }

        public static int CountOf(this string message, string character)
        {
            return message.CountOf(char.Parse(character));
        }

        public static string Dump(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
