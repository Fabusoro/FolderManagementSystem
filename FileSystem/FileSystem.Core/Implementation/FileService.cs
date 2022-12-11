using FileSystem.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ILogger = Serilog.ILogger;

namespace FileSystem.Core.Implementation
{
    public class FileService : IFileService
    {

        private readonly ILogger _logger;

        public FileService(IServiceProvider provider)
        {
            _logger = provider.GetRequiredService<ILogger>();
        }


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
                    _logger.Information("folder not found");
                    return "folder not found";
                }
                var filePath = Path.Combine(folderPath, formFile.FileName);
                using (FileStream fs = File.Create(filePath))
                    _logger.Information("file added");
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
                    _logger.Information("folder not found");
                    return "folder not found";
                }
                if (filePath == null)
                {
                    return "specify file path";
                }
                File.Delete(filePath);
                _logger.Information("file successfully deleted");
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
                    _logger.Information("Could not get file names");
                    throw new Exception("folder does not exist");
                }
                List<string> paths = new List<string>();
                var files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    var d = (Path.GetFileName(file));
                    paths.Add(d);
                }
                _logger.Information("Got all file names from directory");
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
                    _logger.Information("Could not rename file");
                    return "file not found";
                }
                File.Move(Path.Combine(folderPath, fileName), Path.Combine(folderPath, newFileName));
                _logger.Information("File renamed");
                return "update successful";
            }
            catch
            {
                throw;
            }
        }
    }
}
