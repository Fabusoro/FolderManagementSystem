using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Domain
{
    public class Folder
    {        
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
    }
}
