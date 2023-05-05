namespace Register.Helper
{
    public class Utility
    {
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        public static string CurrentDatetime()
        {
            return DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        }
    }
}