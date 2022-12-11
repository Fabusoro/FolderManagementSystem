using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Domain
{
    public class Files
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FolderPath { get; set; }              
    }
}
