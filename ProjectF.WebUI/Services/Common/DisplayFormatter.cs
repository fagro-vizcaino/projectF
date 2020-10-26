namespace ProjectF.WebUI.Services.Common
{
    public static class DisplayFormatter
    {
        public static string DisplayNumberic(decimal n)
            => string.Format("{0:N2}", n);
    }
}
