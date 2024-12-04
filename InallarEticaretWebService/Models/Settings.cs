namespace NotthomePortalApi.Models
{
    public class Settings
    {
        public static string ConnectionString { get; set; } = string.Empty;
        public static string FirmaNo { get; set; } = string.Empty;
        public static string DonemNo { get; set; } = string.Empty;
        public static string ApiAuthKey { get; set; } = string.Empty;
        public static string wkHtmlPath { get; set; } = string.Empty;

        public static ProsesController ProsesController { get; set; } 
    }
}
