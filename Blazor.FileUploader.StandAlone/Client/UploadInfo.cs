using System.ComponentModel.DataAnnotations;

namespace Blazor.FileUploader.StandAlone.Client
{
    public class UploadInfo
    {
        [Required]
        [RegularExpression(@"^[a-z0-9]{1,40}$")]
        [Display(Name ="Organization name")]
        public string ContainerName { get; set; }
    }
}
