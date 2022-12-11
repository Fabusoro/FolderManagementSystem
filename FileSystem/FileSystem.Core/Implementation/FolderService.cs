using AutoMapper;
using FileSystem.Core.Dto;
using FileSystem.Core.Interface;
using FileSystem.Domain;
using ILogger = Serilog.ILogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FileSystem.Core.Implementation
{
    public class FolderService : IFolderService
    {
        private static string rootFolderPath = Directory.GetCurrentDirectory() + "/rootpath";        
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FolderService(IMapper mapper, IServiceProvider provider)
        {            
            _mapper = mapper;
            _logger = provider.GetRequiredService<ILogger>();
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
                    _logger.Information("created default folder");
                    var createdFolder = Directory.CreateDirectory(Path.Combine(rootFolderPath, folderName));
                    return createdFolder.FullName;
                }
                var folderEntity = Directory.CreateDirectory(Path.Combine(rootFolderPath, path, folderName));
                _logger.Information("folder successfully created");
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
                    _logger.Information("folder not found");
                    throw new Exception("no folder found");
                }
                _logger.Information("Got all folders and sub folders");
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
                    _logger.Information("could not update folder");
                    return "no such folder in directory";
                }
                var folderName = folder.FolderName;
                var path = folder.FolderPath;
                Directory.Move(Path.Combine(path, folderName), Path.Combine(path, newName));
                _logger.Information("folder successfully renamed");
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
                    _logger.Information("folder not deleted");
                    return "folder does not exist";
                }
                Directory.Delete(folderPath, true);
                _logger.Information("folder deleted successfully");
                return "folder Successfully deleted.";                
            }
            catch
            {
                throw;
            }           
        }

    }
}
