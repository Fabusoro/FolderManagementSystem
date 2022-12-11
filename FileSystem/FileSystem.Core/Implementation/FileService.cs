using FileSystem.Core.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Core.Implementation
{
    public class FileService : IFileService
    {                
        
        /// <summary>
        /// Interacts with file repository to create files using IFormFile       
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public string AddFile(IFormFile formFile, string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    return "folder not found";
                }
                var filePath = Path.Combine(folderPath, formFile.FileName);
                using (FileStream fs = File.Create(filePath))
                return formFile.Name;
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Interacts with file repository to delete a particular file
        /// </summary>
        /// <param name="filePath"></param>        
        /// <returns></returns>
        public string DeleteFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return "folder not found";
                }
                if (filePath == null)
                {
                    return "specify file path";
                }
                File.Delete(filePath);
                return "sucessfully deleted";
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Interacts with file repository to get file names 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<string> GetFileNamesInFolder(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    throw new Exception("folder does not exist");
                }
                List<string> paths = new List<string>();
                var files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    var d = (Path.GetFileName(file));
                    paths.Add(d);
                }
                return paths;
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Interacts with file repository to rename folder
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        public string RenameFile(string fileName, string folderPath, string newFileName)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    return "file not found";
                }
                File.Move(Path.Combine(folderPath, fileName), Path.Combine(folderPath, newFileName));
                return "update successful";
            }
            catch
            {
                throw;
            }           
        }
    }
}
