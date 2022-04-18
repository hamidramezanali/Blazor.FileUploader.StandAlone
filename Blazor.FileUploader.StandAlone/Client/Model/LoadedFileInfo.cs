using Microsoft.AspNetCore.Components.Forms;

namespace Blazor.FileUploader.StandAlone.Client.Model
{
    public class LoadedFileInfo
    {
        public IBrowserFile BrowserFile { get; set; }
        public string Status { get; set; }
        public LoadedFileInfo(IBrowserFile browserFile, string status)
        {
            BrowserFile = browserFile;
            Status = status;
        }
    }

}
