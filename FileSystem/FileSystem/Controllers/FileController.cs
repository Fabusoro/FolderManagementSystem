using FileSystem.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Interacts with file repository to get file names 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>        
        [HttpGet]
        public IActionResult GetFileNamesInFolder(string folderPath)
        {
            return Ok(_fileService.GetFileNamesInFolder(folderPath));
        }

        /// <summary>
        /// Interacts with file service to create files using IFormFile       
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateFile(IFormFile formFile, string folderPath)
        {
            return Ok(_fileService.AddFile(formFile, folderPath));
        }

        /// <summary>
        /// Interacts with file repository to rename folder
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult RenameFile(string fileName, string folderPath, string newFileName)
        {
            return Ok(_fileService.RenameFile(fileName, folderPath, newFileName));
        }

        /// <summary>
        /// Interacts with file service to delete a particular file
        /// </summary>
        /// <param name="filePath"></param>        
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteFile(string filePath)
        {
            return Ok(_fileService.DeleteFile(filePath));
        }
    }
}
