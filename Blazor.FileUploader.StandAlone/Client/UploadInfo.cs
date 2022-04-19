using System.ComponentModel.DataAnnotations;

namespace Blazor.FileUploader.StandAlone.Client
{
    public class UploadInfo
    {
        [Required]
        [RegularExpression(@"^[a-z0-9]{1,40}$",ErrorMessage ="Only small characters, numbers or - can be used in the field")]
        [Display(Name ="organization name")]
        public string ContainerName { get; set; }
    }
}
