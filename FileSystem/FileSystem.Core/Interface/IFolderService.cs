using FileSystem.Core.Dto;
using FileSystem.Domain;

namespace FileSystem.Core.Interface
{
    public interface IFolderService
    {
        string CreateFolder(FolderDto folderDto);
        string DeleteFolder(FolderDto folderDto);
        IEnumerable<string> GetAFolder(string path);
        string UpdateFolder(FolderDto folderDto, string newName);
    }
}