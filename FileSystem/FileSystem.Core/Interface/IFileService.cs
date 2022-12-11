using Microsoft.AspNetCore.Http;

namespace FileSystem.Core.Interface
{
    public interface IFileService
    {
        string AddFile(IFormFile formFile, string folderPath);
        string DeleteFile(string filePath);
        List<string> GetFileNamesInFolder(string folderPath);
        string RenameFile(string fileName, string folderPath, string newFileName);
    }
}