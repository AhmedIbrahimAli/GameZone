using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.ViewModels
{
    public class GameEditViewModel : GameViewModel
    {
        public int Id { get; set; }

        public string? DisplayImgString { get; set; }


        [AllowedExtentions(FileSettings.allowedExtentions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
