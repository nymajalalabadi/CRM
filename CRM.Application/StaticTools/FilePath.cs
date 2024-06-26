namespace CRM.Application.StaticTools
{
    public static class FilePath
    {
        public static readonly string UploadImageProfile = "/images/user/profile/";
        public static readonly string UploadImageProfileServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/profile/");
    }

}
