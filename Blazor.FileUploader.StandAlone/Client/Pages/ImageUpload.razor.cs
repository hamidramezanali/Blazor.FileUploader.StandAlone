using Blazor.FileUploader.StandAlone.Client.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Blazor.FileUploader.StandAlone.Client.Pages
{
    public partial class ImageUpload
    {
        [Inject]
        public HttpClient HttpClient { get; set; }



        private List<LoadedFileInfo> loadedFiles = new();

        private int maxFileSize = 100000000;
        private int maxAllowedFiles = 20;
        private bool isLoading;
        private string Status = "Ready";
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }


        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            try
            {
                isLoading = true;
                loadedFiles.Clear();

                foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
                {
                    try
                    {
                        loadedFiles.Add(new LoadedFileInfo(file,"Ready to be uploaded"));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("File: {Filename} Error: {Error}",
                            file.Name, ex.Message);
                    }
                }
                isLoading = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task TryToUpload(LoadedFileInfo fileInfo,UploadInfo uploadinfo)
        {

                try
                {
              
                fileInfo.Status = "Uploading";
                    var Id = uploadinfo.ContainerName;
                    
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");                   
                    content.Add(new StreamContent(fileInfo.BrowserFile.OpenReadStream(maxFileSize), Convert.ToInt32(fileInfo.BrowserFile.Size)), Id, fileInfo.BrowserFile.Name);
                    var response = await HttpClient.PostAsync("upload", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if(response.IsSuccessStatusCode)
                      fileInfo.Status =  "Done";
                    else fileInfo.Status = "Failed";
            }
             
                     catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    fileInfo.Status = "Failed";
                }
          
        }
       
        
    }
}
