using AutoMapper;
using FileSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Core.Dto
{
    public class FileSystemProfile : Profile
    {
        //Maps the Entities in to Data transfer objects
        public FileSystemProfile()
        {
            CreateMap<FolderDto, Folder>().ReverseMap();                                      
        }
    }
}
