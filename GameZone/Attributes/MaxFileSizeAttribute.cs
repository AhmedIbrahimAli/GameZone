using GameZone.Settings;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public MaxFileSizeAttribute(int  maxSize)
        {
            _maxSize = maxSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
               

                if (file.Length > _maxSize)
                    return new ValidationResult($"Max Size Allowed is {FileSettings.MaxFileSizeInMB} MB!");
            }

            return ValidationResult.Success;
        }
    }
}
