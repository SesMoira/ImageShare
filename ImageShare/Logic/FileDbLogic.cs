using ImageShare.Models;
using System;
using ImageShare.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageShare.Logic
{
    public class FileDbLogic
    {
        public async Task<bool> UploadImage(FileDb fileDbModel, string filepath)
        {
            FileDbStorageRepo fileDbStorageRepo = new FileDbStorageRepo();
            try
            {
                var filepathSplit = filepath.Split("\\");
                //string filename = new filepathSplit[filepathSplit.Length - 1];

                //string blobURL = ImageStorageRepository.WriteImageToBlob(filepath,filename);

                //bool result = await ImageRepository.CreateImageAsync(ImageModel);
                //return bool result;

            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        //public List<FileDb> SearchBooks(string Title, string geolocation)
        //{
        //    return FileDb.SearchBook(Title, geolocation);
        //}
    }
}
