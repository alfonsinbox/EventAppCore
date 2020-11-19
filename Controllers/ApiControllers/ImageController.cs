using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;

namespace EventAppCore.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Website()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {   
            // Get files from form data
            var files = Request.Form.Files;

            // You have to send one file, otherwise it's a bad request
            if (files.Count != 1)
            {
                return BadRequest($"Upload exactly 1 file, you uploaded {files.Count} file(s)");
            }

            // You have to send one file, otherwise it's a bad request
            if (files[0].ContentType != "image/jpeg")
            {
                return BadRequest($"Content Type should be image/jpeg. You used {files[0].ContentType}");
            }

            // Instantiate the storage account on Azure, gives access to all containers / blobs
            var storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                    "nittiet",
                    "O82ZfLZyblX108HD+sKtaZibcQRWSb6rOaBbFAeuJKrzUyAiURZjNCeShvJAnaR5dQFVHmK50g4xH4HgQiarEw=="), true);

            // Create a blob client.§
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container named "image-store."
            var container = blobClient.GetContainerReference("image-store");

            var imageName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";

            // Get a reference to a blob with image name.
            var blockBlob = container.GetBlockBlobReference(imageName);

            // Upload every file (but there should only be one)
            foreach (var file in files)
            {
                await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            }
            
            // Get size of all files
            var size = (from file in files
                select file.Length).Sum();

            // Return message, reveales file count and total size
            var message = $"{files.Count} file(s) / {size} bytes uploaded successfully!";

            // Return the randomly generated image name
            return Json(blockBlob.StorageUri.PrimaryUri);
        }
    }
}