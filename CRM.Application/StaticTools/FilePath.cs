namespace CRM.Application.StaticTools
{
    public static class FilePath
    {
        #region User

        public static readonly string UserProfileDefault = "/images/user/default/avatar.png";
        public static readonly string UploadImageProfile = "/images/user/profile/";
        public static readonly string UploadImageProfileServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user/profile/");

        #endregion

        #region Order

        public static readonly string OrderImagePath = "/images/order/image/";
        public static readonly string OrderImagePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/order/image/");

        #endregion

    }

}
