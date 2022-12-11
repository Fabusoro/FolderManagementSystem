using FileSystem.Core.Dto;
using FileSystem.Core.Interface;
using FileSystem.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        /// <summary>
        /// Gets folder names and their sub folders
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFolder(string path)
        {
            var folder = _folderService.GetAFolder(path);
            return Ok(folder);
        }       

        /// <summary>
        /// Creates folders using folder paths
        /// </summary>
        /// <param name="folderDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateFolder([FromQuery]FolderDto folderDto)
        {
             var result = _folderService.CreateFolder(folderDto);  
            return Ok(result);
        }
        /// <summary>
        /// Renames folder using the parent folder path
        /// </summary>
        /// <param name="folderDto"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult UpdateFolder([FromQuery]FolderDto folderDto, string newName)
        {
            var result = _folderService.UpdateFolder(folderDto, newName);
            return Ok(result);  
        }
        
        /// <summary>
        /// Deletes folder using folder folder path
        /// </summary>
        /// <param name="folderDto"></param>
        [HttpDelete]
        public void DeleteFolder([FromQuery] FolderDto folderDto)
        {
            _folderService.DeleteFolder(folderDto);
        } 
    }
}
