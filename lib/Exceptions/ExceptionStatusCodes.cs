namespace WebshopAPI.lib.Exceptions
{
    public static class ExceptionStatusCodes
    {
        public static readonly int ITEM_NOT_FOUND = 113;
        public static readonly int ITEM_ALREADY_EXISTS = 112;
        public static readonly int BAD_OLD_PASSWORD = 111;
        public static readonly int USER_PASSWORD_AND_NEW_PASSWORD_NOT_SAME = 110;
        public static readonly int PASSWORD_AND_REPEATED_PASSWORD_NOT_MATCH = 109;
        public static readonly int EMAIL_NOT_VALID = 108;
        public static readonly int USER_ALREADY_EXISTS = 107;
        public static readonly int BODY_EMPTY = 106;
        public static readonly int PASSWORD_NOT_MATCH = 105;
        public static readonly int PASSWORD_NOT_FOUND = 104;
        public static readonly int USER_NOT_ACTIVATED = 103;
        public static readonly int USER_NOT_FOUND = 102;
        public static readonly int MANDATORY_DATA_EMPTY = 101;
    }
}
