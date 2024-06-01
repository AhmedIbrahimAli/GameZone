namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string imagesFolder = "/assets/images/Games";
        public const string allowedExtentions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
