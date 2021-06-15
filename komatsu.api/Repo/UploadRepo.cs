using komatsu.api.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace komatsu.api.Repo
{
    public class UploadRepo : IUploadRepo
    {
        public async Task<string> Upload(List<IFormFile> files)
        {
            try
            {
                string fullPath = "";
                string fileName = "";
                //List<PhotoFileName> photoView = null;

                if (files.Count > 0)
                {
                    string folderName = Path.Combine("upload", "image");
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    var fileNameArray = new List<String>(); // multiple images case

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    foreach (var formFile in files)
                    {
                        fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(formFile.FileName); // unique name
                        // path.combine ใส่ \\ หรือ // ให้เอง 
                        fullPath = Path.Combine(filePath, fileName);

                        if (formFile.Length > 0)
                        {
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream); // อัพโหลดรูปแบบไม่รอ แต่จะใช้ cpu เยอะ แต่ได้ไฟล์ชัว
                            }
                        }

                        fileNameArray.Add(fileName); // multiple images case
                    }

                    //photoView = new List<PhotoFileName>();
                    //PhotoFileName photoFileName;

                    //foreach (var img in fileNameArray)
                    //{
                    //    photoFileName = new PhotoFileName()
                    //    {
                    //        FileNames = img,
                    //        Paths = folderName
                    //    };

                    //    photoView.Add(photoFileName);
                    //}
                    string path = string.Format("https://komatsu-api.dev.fysvc.com/{0}/{1}", "upload/image", fileName);
                    return path;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
