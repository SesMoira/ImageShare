using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageShare.Repository
{
    public class FileDbStorageRepo
    {
        public string ReadImageFromBlob(string filename)
        {
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=myimageshare;AccountKey=Dr6ctDMN+Vy8yUpRFxxsRC4rX+geMLZSnbbBDpsEJBT5KF35dTPtqX0gk4KotS5+sI9hA8GwrMMS7J0YQmDHzg==;EndpointSuffix=core.windows.net";
            string container = "ImageShare";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionstring, container);
            var blob = blobContainerClient.GetBlobClient(filename);

            string filePath = @$"C:\Users\izwelethu\source\repos\ImageShare\ImageShare\wwwroot\image\{filename}";

            blob.DownloadTo(filePath);

            return filePath;
        }

        public string WriteImageToBlob(string filePath, string filename)
        {
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=myimageshare;AccountKey=Dr6ctDMN+Vy8yUpRFxxsRC4rX+geMLZSnbbBDpsEJBT5KF35dTPtqX0gk4KotS5+sI9hA8GwrMMS7J0YQmDHzg==;EndpointSuffix=core.windows.net";
            string container = "ImageShare";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionstring, container);


            StreamReader Sr = new StreamReader(filePath);


            blobContainerClient.UploadBlob(filename, Sr.BaseStream);

            return blobContainerClient.Uri.ToString();
        }
    }
}
