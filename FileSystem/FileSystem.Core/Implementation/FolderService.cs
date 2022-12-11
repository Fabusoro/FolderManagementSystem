using AutoMapper;
using FileSystem.Core.Dto;
using FileSystem.Core.Interface;
using FileSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Core.Implementation
{
    public class FolderService : IFolderService
    {
        private static string rootFolderPath = Directory.GetCurrentDirectory() + "/rootpath";        
        private readonly IMapper _mapper;

        public FolderService(IMapper mapper)
        {            
            _mapper = mapper;
        }

        /// <summary>
        /// Interacts with repository to create folders 
        /// </summary>
        /// <param name="folderDto"></param>
        /// <returns></returns>
        public string CreateFolder(FolderDto folderDto)
        {
            try
            {
                var folder = _mapper.Map<Folder>(folderDto);
                var folderName = folder.FolderName;
                var path = folder.FolderPath;
                if (path == null)
                {
                    var createdFolder = Directory.CreateDirectory(Path.Combine(rootFolderPath, folderName));
                    return createdFolder.FullName;
                }
                var folderEntity = Directory.CreateDirectory(Path.Combine(rootFolderPath, path, folderName));
                return folderEntity.Name;
            }
            catch
            {
                throw;
            }
           
        }

        /// <summary>
        /// Interacts with repository to get folder names including folders and sub-folders
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<string> GetAFolder(string path)
        {
            try
            {
                var folder = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);                
                if (folder == null)
                {
                    throw new Exception("no folder found");
                }
                return folder;
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Interacts with repository to rename folder
        /// </summary>
        /// <param name="folderDto"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public string UpdateFolder(FolderDto folderDto, string newName)
        {
            try
            {
                var folder = _mapper.Map<Folder>(folderDto);
                if (!Directory.Exists(folder.FolderPath))
                {
                    return "no such folder in directory";
                }
                var folderName = folder.FolderName;
                var path = folder.FolderPath;
                Directory.Move(Path.Combine(path, folderName), Path.Combine(path, newName));
                return "updated";
            }
            catch
            {
                throw;
            }            
        }

        /// <summary>
        /// Interacts with repository to delete a particular folder
        /// </summary>
        /// <param name="folderDto"></param>
        /// <returns></returns>
        public string DeleteFolder(FolderDto folderDto)
        {
            try
            {
                var folder = _mapper.Map<Folder>(folderDto);                
                var folderPath = folder.FolderPath;
                if (!Directory.Exists(folderPath))
                {
                    return "folder does not exist";
                }
                Directory.Delete(folderPath, true);
                return "folder Successfully deleted.";                
            }
            catch
            {
                throw;
            }           
        }

    }
}
